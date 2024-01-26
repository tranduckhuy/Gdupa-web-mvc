using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl
{
    public class WarehouseSerivce : IWarehouseService
    {
        private readonly DataContext _dataContext;
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;

        public WarehouseSerivce(DataContext dataContext, IReceiptService receiptService, IMapper mapper)
        {
            _dataContext = dataContext;
            _receiptService = receiptService;
            _mapper = mapper;
        }

        private bool IsExisted(long productId)
        {
            return _dataContext.Products.FirstOrDefault(p => p.ProductId == productId) != null ? true : false;
        }

        private bool ImportProducts(ICollection<Warehouse> importProducts)
        {
            if (importProducts == null || importProducts.Count == 0)
            {
                return false;
            }

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
                                new Warehouse
                                {
                                    ProductId = importProduct.ProductId,
                                    Quantity = importProduct.Quantity,
                                    QuantityAtBeginPeriod = 0,
                                    QuantityImport = importProduct.Quantity,
                                    PriceImport = importProduct.PriceImport
                                }
                            );
                            break;
                        }
                        else
                        {
                            var currentQuarter = GetQuarter(DateTime.Now);
                            var existingProduct = product.FirstOrDefault(p => ((p.CreatedAt.Month - 1) / 3 + 1) == currentQuarter
                                                        && p.CreatedAt.Year == DateTime.Now.Year);
                            if (existingProduct == null)
                            {
                                if (currentQuarter > 1)
                                {
                                    existingProduct = product.FirstOrDefault(p => ((p.CreatedAt.Month - 1) / 3 + 1) == (currentQuarter - 1)
                                                            && p.CreatedAt.Year == DateTime.Now.Year);
                                }
                                else
                                {
                                    existingProduct = product.FirstOrDefault(p => ((p.CreatedAt.Month - 1) / 3 + 1) == 4
                                                            && p.CreatedAt.Year == DateTime.Now.Year - 1);
                                }

                                // If there is still an implicit error, return false
                                if (existingProduct == null)
                                {
                                    return false;
                                }

                                importProduct.QuantityAtBeginPeriod = existingProduct.Quantity;
                                importProduct.QuantityImport = importProduct.Quantity;
                                importProduct.Quantity += existingProduct.Quantity;
                                _dataContext.Warehouse.Add(importProduct);
                            }
                            else
                            {
                                existingProduct.Quantity += importProduct.Quantity;
                                existingProduct.QuantityImport += importProduct.Quantity;
                                existingProduct.PriceImport = importProduct.PriceImport;
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

        private static int GetQuarter(DateTime dateTime)
        {
            return (dateTime.Month - 1) / 3 + 1;
        }

        public bool Add(ImportProductsDTO importProducts)
        {
            if (!ImportProducts(importProducts.ImportProducts))
            {
                return false;
            }
            _receiptService.Add(importProducts);
            return true;
        }

        public WarehouseViewModel GetLimit(int page, int quarter, int year)
        {

            if (quarter == 0 && year == 0)
            {
                quarter = (DateTime.UtcNow.Month - 1) / 3 + 1;
                year = DateTime.UtcNow.Year;
            }

            if (quarter < 1 || quarter > 4 || year < 1990 || year > DateTime.UtcNow.Year + 10) { return null!; }

            const int pageSize = 5;
            if (page < 1)
            {
                page = 1;
            }

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            var totalProducts = _dataContext.Warehouse.Count(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate);
            var pageable = new Pageable(totalProducts, page, pageSize);
            int skipAmount = (pageable.CurrentPage - 1) * pageSize;

            var warehouse = _dataContext.Warehouse
                .AsNoTracking()
                .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.Product)
                .OrderBy(w => w.WarehouseId)
                .ToList();

            int currentQuarter = (DateTime.UtcNow.Month - 1) / 3 + 1;
            startDate = new DateTime(DateTime.UtcNow.Year, (currentQuarter - 1) * 3 + 1, 1);
            endDate = startDate.AddMonths(3).AddDays(-1);

            int lowAlert = _dataContext.Warehouse.Count(w => w.Quantity < 10 && w.Quantity > 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate);
            int outOfStock = _dataContext.Warehouse.Count(w => w.Quantity == 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate);

            var uniqueYears = _dataContext.Warehouse
            .Select(w => w.CreatedAt.Year)
            .Distinct()
            .ToList();

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

        public WarehouseImportViewModel GetDataViewImport()
        {
            var products = _dataContext.Products
                .ToList();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);
            var suppliers = _dataContext.Suppliers.ToList();

            return new WarehouseImportViewModel { Products = productsDTO, Suppliers = suppliers };
        }

        public WarehouseViewModel GetByStatus(string status)
        {
            int quarter = (DateTime.UtcNow.Month - 1) / 3 + 1;
            int year = DateTime.UtcNow.Year;

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            List<Warehouse> warehouse;
            WarehouseViewModel warehouseViewModel = new();

            switch (status)
            {
                case AppConstant.LOW_ALERT:
                    warehouse = _dataContext.Warehouse
                                .AsNoTracking()
                                .Where(w => w.Quantity < 10 && w.Quantity > 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                .Include(p => p.Product)
                                .OrderBy(w => w.WarehouseId)
                                .ToList();
                    warehouseViewModel.LowAlert = warehouse.Count;
                    warehouseViewModel.Title = "Low-Stock Products";
                    break;
                case AppConstant.OUT_OF_STOCK:
                    warehouse = _dataContext.Warehouse
                                .AsNoTracking()
                                .Where(w => w.Quantity == 0 && w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                .Include(p => p.Product)
                                .OrderBy(w => w.WarehouseId)
                                .ToList();
                    warehouseViewModel.OutOfStock = warehouse.Count;
                    warehouseViewModel.Title = "Out-of-Stock Products";
                    break;
                default:
                    return null!;
            }
            warehouseViewModel.Warehouses = warehouse;

            return warehouseViewModel;
        }

        public WarehouseViewModel SearchProduct(string searchType, string searchValue)
        {
            int quarter = (DateTime.UtcNow.Month - 1) / 3 + 1;
            int year = DateTime.UtcNow.Year;

            DateTime startDate = new(year, (quarter - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);
            IQueryable<Warehouse> searchProduct = _dataContext.Warehouse
                                .AsNoTracking()
                                .Where(w => w.CreatedAt >= startDate && w.CreatedAt <= endDate)
                                .Include(p => p.Product)
                                .OrderBy(w => w.WarehouseId);

            switch (searchType)
            {
                default:
                    searchProduct = searchProduct.Where(w => w.Product.Name.ToUpper().Contains(searchValue.ToUpper()));
                    break;
            }

            if (searchProduct.Any())
            {
                var warehouseViewModel = new WarehouseViewModel
                {
                    Warehouses = searchProduct.ToList(),
                    Quarter = quarter,
                    Year = year
                };
                warehouseViewModel.ImportYears.Add(year);
                warehouseViewModel.Title = "Search";
                return warehouseViewModel;
            }
            return null!;
        }
    }
}
