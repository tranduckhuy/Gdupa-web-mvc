using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Data;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared;
using Warehouse.Shared.DTOs;
using Warehouse.Shared.DTOs.ProductDTO;
using Warehouse.Shared.ViewModels;

namespace WarehouseWebMVC.Services
{
    public class WarehouseSerivce : IWarehouseService
    {
        private readonly DataContext _dataContext;
        private readonly IImportNoteService _importNoteService;
        private readonly IMapper _mapper;
        private static readonly object _lockObject = new object();

        public WarehouseSerivce(DataContext dataContext, IImportNoteService importNoteService, IMapper mapper)
        {
            _dataContext = dataContext;
            _importNoteService = importNoteService;
            _mapper = mapper;
        }

        private bool IsExisted(long productId)
        {
            return _dataContext.Products
                .FirstOrDefault(p => p.ProductId == productId && p.IsDiscontinued == false) != null ? true : false;
        }

        private bool ImportProducts(ICollection<WarehouseE> importProducts)
        {
            if (importProducts == null || importProducts.Count == 0)
            {
                return false;
            }

            lock (_lockObject)
            {
                try
                {
                    using var transaction = _dataContext.Database.BeginTransaction();
                    try
                    {
                        foreach (var importProduct in importProducts)
                        {
                            if (!IsExisted(importProduct.ProductId))
                            {
                                transaction.Rollback();
                                return false;
                            }

                            var product = _dataContext.Warehouse
                                .Where(p => p.ProductId == importProduct.ProductId)
                                .ToList();

                            if (product.Count == 0)
                            {
                                _dataContext.Warehouse.Add(
                                    new WarehouseE
                                    {
                                        ProductId = importProduct.ProductId,
                                        Quantity = importProduct.Quantity,
                                        QuantityAtBeginPeriod = 0,
                                        QuantityImport = importProduct.Quantity,
                                        PriceImport = importProduct.PriceImport,
                                        CreatedAt = DateTime.UtcNow.ToLocalTime()
                                    }
                                );
                                break;
                            }
                            else
                            {
                                var currentQuarter = GetQuarter(DateTime.UtcNow.ToLocalTime());
                                var existingProduct = product.FirstOrDefault(p => (p.CreatedAt.Month - 1) / 3 + 1 == currentQuarter
                                                            && p.CreatedAt.Year == DateTime.UtcNow.ToLocalTime().Year);
                                if (existingProduct != null)
                                {
                                    existingProduct.Quantity += importProduct.Quantity;
                                    existingProduct.QuantityImport += importProduct.Quantity;
                                    existingProduct.PriceImport = importProduct.PriceImport;
                                    existingProduct.CreatedAt = DateTime.UtcNow.ToLocalTime();
                                }
                            }
                            _dataContext.SaveChanges();
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        private static int GetQuarter(DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }

        public bool Add(ImportProductsDTO importProducts)
        {
            if (importProducts == null || importProducts.Deliverer.Length == 0)
            {
                return false;
            }

            if (!ImportProducts(importProducts.ImportProducts))
            {
                return false;
            }

            _importNoteService.Add(importProducts);
            return true;
        }

        public async Task<WarehouseImportViewModel> GetDataViewImportAsync()
        {
            var products = await _dataContext.Products
                .Where(p => p.IsDiscontinued == false)
                .ToListAsync();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);
            var suppliers = await _dataContext.Suppliers.ToListAsync();

            return new WarehouseImportViewModel { Products = productsDTO, Suppliers = suppliers };
        }


        public async Task<WarehouseViewModel> GetByStatusAsync(string status)
        {
            int quarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
            int year = DateTime.UtcNow.ToLocalTime().Year;

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            List<WarehouseE> warehouse;
            WarehouseViewModel warehouseViewModel = new();

            switch (status)
            {
                case AppConstant.LOW_ALERT:
                    warehouse = await _dataContext.Warehouse
                                .AsNoTracking()
                                .Where(w => w.Quantity < 10 && w.Quantity > 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                .Include(p => p.Product)
                                .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0))
                                .OrderBy(w => w.WarehouseId)
                                .ToListAsync();
                    warehouseViewModel.LowAlert = warehouse.Count;
                    warehouseViewModel.Title = "Low-Stock Products";
                    break;
                case AppConstant.OUT_OF_STOCK:
                    warehouse = await _dataContext.Warehouse
                                .AsNoTracking()
                                .Where(w => w.Quantity == 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                .Include(p => p.Product)
                                .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0))
                                .OrderBy(w => w.WarehouseId)
                                .ToListAsync();
                    warehouseViewModel.OutOfStock = warehouse.Count;
                    warehouseViewModel.Title = "Out-of-Stock Products";
                    break;
                default:
                    return null!;
            }
            warehouseViewModel.Warehouses = warehouse;

            return warehouseViewModel;
        }

        public WarehouseViewModel SearchProduct(string searchType, string searchValue, int quarter, int year)
        {
            if (quarter == 0 && year == 0)
            {
                quarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
                year = DateTime.UtcNow.ToLocalTime().Year;
            }

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);
            IQueryable<WarehouseE> searchProduct;

            switch (searchType)
            {
                default:
                    var query = $"SELECT * " +
                                $"FROM Warehouse " +
                                $"INNER JOIN Products ON Warehouse.ProductId = Products.ProductId " +
                                $"WHERE Products.Name COLLATE NOCASE LIKE '%' || @searchValue || '%' " +
                                $"ORDER BY WarehouseId";

                    searchProduct = _dataContext.Warehouse.FromSqlRaw(query, new SqliteParameter("@searchValue", searchValue))
                                                          .AsNoTracking()
                                                          .Include(p => p.Product)
                                                          .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                                          .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0));
                    break;
            }

            if (searchProduct.Any())
            {
                var uniqueYears = _dataContext.Warehouse
               .Select(w => w.CreatedAt.Year)
               .Distinct()
               .ToList();

                var warehouseViewModel = new WarehouseViewModel
                {
                    Warehouses = searchProduct.ToList(),
                    Quarter = quarter,
                    Year = year,
                    ImportYears = uniqueYears
                };
                warehouseViewModel.Title = "Search";
                return warehouseViewModel;
            }
            return null!;
        }

        public bool CheckNewQuarter()
        {
            int currentQuarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
            int currentYear = DateTime.UtcNow.ToLocalTime().Year;

            var latestQuarter = _dataContext.Warehouse
                .OrderByDescending(w => w.CreatedAt)
                .Select(w => new
                {
                    w.CreatedAt.Year,
                    Quarter = (w.CreatedAt.Month - 1) / 3 + 1
                })
                .FirstOrDefault();

            if (latestQuarter != null)
            {
                if (latestQuarter.Year == currentYear && latestQuarter.Quarter < currentQuarter || latestQuarter.Year < currentYear)
                {
                    UpdateProductsToNewQuarter(latestQuarter.Quarter, latestQuarter.Year);
                    return true;
                }
            }

            return false;
        }

        private void UpdateProductsToNewQuarter(int quarter, int year)
        {
            lock (_lockObject)
            {
                DateTime startDate = new DateTime(year, (quarter - 1) * 3 + 1, 1);
                DateTime endDate = startDate.AddMonths(3).AddDays(-1);

                var warehouse = _dataContext.Warehouse
                    .AsNoTracking()
                    .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                    .Include(p => p.Product)
                    .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0))
                    .OrderBy(w => w.WarehouseId)
                    .ToList();

                try
                {
                    using var transaction = _dataContext.Database.BeginTransaction();
                    try
                    {
                        foreach (var oldQuarterProductInfo in warehouse)
                        {
                            _dataContext.Warehouse.Add(
                                new WarehouseE
                                {
                                    ProductId = oldQuarterProductInfo.ProductId,
                                    Quantity = oldQuarterProductInfo.Quantity,
                                    QuantityAtBeginPeriod = oldQuarterProductInfo.Quantity,
                                    QuantityImport = 0,
                                    PriceImport = oldQuarterProductInfo.PriceImport
                                }
                            );
                            _dataContext.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Task<byte[]> ExportDataToExcel(int quarter, int year)
        {
            return Task.Run(() =>
            {
                byte[] fileBytes = GenerateExcelFile(quarter, year);

                return fileBytes;
            });
        }

        private byte[] GenerateExcelFile(int quarter, int year)
        {
            if (quarter == 0 && year == 0)
            {
                quarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
                year = DateTime.UtcNow.ToLocalTime().Year;
            }

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            List<WarehouseE> warehouseData = _dataContext.Warehouse
                .AsNoTracking()
                .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                .Include(p => p.Product)
                .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0))
                .OrderBy(w => w.WarehouseId)
                .ToList();

            Dictionary<int, string> quarterMap = new Dictionary<int, string>
            {
                {1, "First"},
                {2, "Second"},
                {3, "Third"},
                {4, "Fourth"}
            };
            string quarterText = quarterMap.TryGetValue(quarter, out string? value) ? value : quarter.ToString();

            // New Excel Package
            using (var package = new ExcelPackage())
            {
                // New Sheet
                var worksheet = package.Workbook.Worksheets.Add($"{quarterText} Quarter - {year}");

                // Title
                worksheet.Cells[1, 1].Value = "Product";
                worksheet.Cells[1, 2].Value = "Starting Quantity";
                worksheet.Cells[1, 3].Value = "Import Quantity";
                worksheet.Cells[1, 4].Value = "Import Price";
                worksheet.Cells[1, 5].Value = "Current Quantity";
                worksheet.Cells[1, 6].Value = "Unit";
                worksheet.Cells[1, 7].Value = "Total Stock Price";

                // Data for each title
                for (int i = 0; i < warehouseData.Count; i++)
                {
                    WarehouseE warehouse = warehouseData.ElementAt(i);
                    worksheet.Cells[i + 2, 1].Value = warehouse.Product.Name;
                    worksheet.Cells[i + 2, 2].Value = warehouse.QuantityAtBeginPeriod;
                    worksheet.Cells[i + 2, 3].Value = warehouse.QuantityImport;
                    worksheet.Cells[i + 2, 4].Value = Math.Round(warehouse.PriceImport * warehouse.QuantityImport, 2);
                    worksheet.Cells[i + 2, 5].Value = warehouse.Quantity;
                    worksheet.Cells[i + 2, 6].Value = warehouse.Product.Unit;
                    worksheet.Cells[i + 2, 7].Value = warehouse.PriceImport * warehouse.Quantity;
                }

                byte[] fileBytes = package.GetAsByteArray();
                return fileBytes;
            }
        }

        public async Task<WarehouseViewModel> GetLimitAsync(int page, int quarter, int year)
        {
            if (quarter == 0 && year == 0)
            {
                quarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
                year = DateTime.UtcNow.ToLocalTime().Year;
            }

            if (quarter < 1 || quarter > 4 || year < 1990 || year > DateTime.UtcNow.ToLocalTime().Year) { return null!; }

            const int pageSize = 5;
            if (page < 1)
            {
                page = 1;
            }

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            var totalProducts = await _dataContext.Warehouse.CountAsync(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate);
            var pageable = new Pageable(totalProducts, page, pageSize);
            int skipAmount = (pageable.CurrentPage - 1) * pageSize;
            if (skipAmount < 0)
            {
                skipAmount = 0;
            }

            var warehouse = await _dataContext.Warehouse
                .AsNoTracking()
                .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.Product)
                .Where(w => w.Product.IsDiscontinued == false || (w.Product.IsDiscontinued && w.Quantity > 0))
                .OrderBy(w => w.WarehouseId)
                .ToListAsync();

            int currentQuarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
            startDate = new DateTime(DateTime.UtcNow.ToLocalTime().Year, (currentQuarter - 1) * 3 + 1, 1);
            endDate = startDate.AddMonths(3).AddDays(-1);

            int lowAlert = await _dataContext.Warehouse.CountAsync(w => w.Quantity < 10 && w.Quantity > 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate);
            int outOfStock = await _dataContext.Warehouse.CountAsync(w => w.Quantity == 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate);

            var uniqueYears = await _dataContext.Warehouse
            .Select(w => w.CreatedAt.Year)
            .Distinct()
            .ToListAsync();

            return new WarehouseViewModel
            {
                Warehouses = warehouse,
                LowAlert = lowAlert,
                OutOfStock = outOfStock,
                Pageable = new Pageable(totalProducts, page, pageSize),
                Quarter = quarter,
                Year = year,
                ImportYears = uniqueYears
            };
        }

        public async Task<bool> CheckNewQuarterAsync()
        {
            int currentQuarter = (DateTime.UtcNow.ToLocalTime().Month - 1) / 3 + 1;
            int currentYear = DateTime.UtcNow.ToLocalTime().Year;

            var latestQuarter = await _dataContext.Warehouse
                .OrderByDescending(w => w.CreatedAt)
                .Select(w => new
                {
                    w.CreatedAt.Year,
                    Quarter = (w.CreatedAt.Month - 1) / 3 + 1
                })
                .FirstOrDefaultAsync();

            if (latestQuarter != null)
            {
                if (latestQuarter.Year == currentYear && latestQuarter.Quarter < currentQuarter || latestQuarter.Year < currentYear)
                {
                    UpdateProductsToNewQuarter(latestQuarter.Quarter, latestQuarter.Year);
                    return true;
                }
            }

            return false;
        }

    }
}
