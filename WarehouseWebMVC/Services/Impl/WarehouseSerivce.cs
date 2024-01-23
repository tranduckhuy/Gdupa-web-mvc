using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl
{
    public class WarehouseSerivce : IWarehouseService
    {
        private readonly DataContext _dataContext;

        public WarehouseSerivce(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(List<Warehouse> importProducts)
        {
            if (importProducts == null ||  importProducts.Count == 0)
            {
                return false;
            }

            try
            {
                foreach (var importProduct in importProducts)
                {
                    _dataContext.Warehouse.Add(importProduct);
                }
                _dataContext.SaveChanges();
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool Add(ImportProductsDTO importProducts)
        {
            throw new NotImplementedException();
        }

        public WarehouseViewModel GetAll(int page)
        {
            var totalProducts = _dataContext.Products.Count();
            const int pageSize = 2;
            if (page < 1)
            {
                page = 1;
            }
            var pageable = new Pageable(totalProducts, page, pageSize);

            int skipAmount = (pageable.CurrentPage - 1) * pageSize;
            var warehouse = _dataContext.Warehouse
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.Product)
                .ThenInclude(p => p.Supplier)
                .OrderBy(w => w.WarehouseId)
                .ToList();

            int lowAlert = _dataContext.Warehouse.Count(w => w.Quantity < 10 && w.Quantity > 0);
            int outOfStock = _dataContext.Warehouse.Count(w => w.Quantity == 0);

            return new WarehouseViewModel
            {
                Warehouses = warehouse,
                LowAlert = lowAlert,
                OutOfStock = outOfStock,
                Pageable = new Pageable(totalProducts, page, pageSize)
            };
        }
    }
}
