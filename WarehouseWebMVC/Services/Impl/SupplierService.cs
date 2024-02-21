using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.SupplierDTO;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl
{
    public class SupplierService : ISupplierService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public SupplierService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public Supplier Add(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public SupplierViewModel GetAll(int page)
        {

            var totalSuppliers = _dataContext.Suppliers.Count();
            const int pageSize = 6;
            if (page < 1)
            {
                page = 1;
            }
            var pageable = new Pageable(totalSuppliers, page, pageSize);

            int skipAmount = (pageable.CurrentPage - 1) * pageSize;

            var suppliers = _dataContext.Suppliers
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.ImportNotes)
                .OrderBy(p => p.SupplierId)
                .ToList();

            var suppliersDto = _mapper.Map<List<SupplierDTO>>(suppliers);

            var supplierViewModel = new SupplierViewModel { Suppliers = suppliersDto, Pageable = pageable };

            return supplierViewModel;
        }


        public SupplierViewModel SearchSupplier(string searchType, string searchValue)
        {
            IQueryable<Supplier> searchSupplier = _dataContext.Suppliers;

            switch (searchType)
            {
                case "Email":
                    searchSupplier = searchSupplier.Where(u => u.Email.ToUpper().Contains(searchValue.ToUpper()));
                    break;

                default:
                    var query = $"SELECT * FROM Suppliers WHERE {searchType} COLLATE NOCASE LIKE '%' || @searchValue || '%'";
                    searchSupplier = _dataContext.Suppliers.FromSqlRaw(query, new SqliteParameter("@searchValue", searchValue));
                    break;
            }

            if (searchSupplier.Any())
            {
                var searchSupplierDto = _mapper.Map<List<SupplierDTO>>(searchSupplier.ToList());

                var supplierViewModel = new SupplierViewModel { Suppliers = searchSupplierDto };
                return supplierViewModel;
            }
            return null!;
        }
    }
}
