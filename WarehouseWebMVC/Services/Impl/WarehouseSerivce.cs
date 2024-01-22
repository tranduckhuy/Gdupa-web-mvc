using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
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
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.Product)
                .Include(s => s.Product.Supplier)
                .OrderBy(w => w.WarehouseId)
                .ToList();

            return new WarehouseViewModel { Warehouses = warehouse, Pageable = pageable };
        }
    }
}
