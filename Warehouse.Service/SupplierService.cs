using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Warehouse.Domain;
using Warehouse.Domain.DTOs;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Interfaces;
using Warehouse.Domain.ViewModels;
using Warehouse.Infrastructure.Data;

namespace WarehouseWebMVC.Services
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

        public SupplierDTO GetById(long supplierId)
        {
            var supplier = _dataContext.Suppliers
            .Include(ipn => ipn.ImportNotes)
            .FirstOrDefault(s => s.SupplierId == supplierId);

            return supplier != null ? _mapper.Map<SupplierDTO>(supplier) : null!;
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

            foreach (var supplierDto in suppliersDto)
            {
                supplierDto.Address = ExtractCityFromAddress(supplierDto.Address);
            }

            var supplierViewModel = new SupplierViewModel { Suppliers = suppliersDto, Pageable = pageable };

            return supplierViewModel;
        }
        public bool AddSupplier(SupplierDTO addSupplierDTO)
        {
            try
            {
                if (IsEmailAlreadyExists(addSupplierDTO.Email))
                {
                    return false;
                }
                addSupplierDTO.Avatar ??= "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
                addSupplierDTO.Address =
                    addSupplierDTO.Street + ", "
                    + (addSupplierDTO.Apartment != null && addSupplierDTO.Apartment != "" ? addSupplierDTO.Apartment + ", " : "")
                    + (addSupplierDTO.Ward != null && addSupplierDTO.Ward != "" ? addSupplierDTO.Ward + ", " : "")
                    + addSupplierDTO.District + ", "
                    + addSupplierDTO.Province;

                var supplier = _mapper.Map<Supplier>(addSupplierDTO);
                supplier.Address = addSupplierDTO.Address;

                _dataContext.Suppliers.Add(supplier);
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateSupplier(SupplierDTO updateSupplierDTO)
        {
            try
            {
                var existingSupplier = _dataContext.Suppliers.FirstOrDefault(s => s.SupplierId == updateSupplierDTO.SupplierId);

                if (existingSupplier == null)
                {
                    return false;
                }

                bool isEmailUpdated = updateSupplierDTO.Email != existingSupplier.Email;

                if (isEmailUpdated && IsEmailAlreadyExists(updateSupplierDTO.Email)
                                    || !isEmailUpdated && !SupplierOwnsInformation(updateSupplierDTO.Email, updateSupplierDTO.SupplierId))
                {
                    return false;
                }

                if (updateSupplierDTO.Avatar == null)
                {
                    updateSupplierDTO.Avatar = existingSupplier.Avatar;
                }

                if (updateSupplierDTO.Background == null)
                {
                    updateSupplierDTO.Background = existingSupplier.Background;
                }

                if (updateSupplierDTO.Name != null && updateSupplierDTO.Phone != null && updateSupplierDTO.Avatar != null && updateSupplierDTO.Background != null)
                {
                    existingSupplier.Name = updateSupplierDTO.Name;
                    existingSupplier.Phone = updateSupplierDTO.Phone;
                    existingSupplier.Fax = updateSupplierDTO.Fax;
                    existingSupplier.Email = updateSupplierDTO.Email;
                    updateSupplierDTO.Address =
                        updateSupplierDTO.Street + ", "
                        + (updateSupplierDTO.Apartment != null && updateSupplierDTO.Apartment != "" ? updateSupplierDTO.Apartment + ", " : "")
                        + (updateSupplierDTO.Ward != null && updateSupplierDTO.Ward != "" ? updateSupplierDTO.Ward + ", " : "")
                        + updateSupplierDTO.District + ", "
                        + updateSupplierDTO.Province;
                    existingSupplier.Address = updateSupplierDTO.Address;
                    existingSupplier.Avatar = updateSupplierDTO.Avatar;
                    existingSupplier.Background = updateSupplierDTO.Background;
                }

                _dataContext.Entry(existingSupplier).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool IsEmailAlreadyExists(string email)
        {
            return _dataContext.Suppliers.Any(s => s.Email == email);
        }
        public bool SupplierOwnsInformation(string supplierEmail, long supplierId)
        {
            var supplier = GetById(supplierId);
            return supplier != null && supplier.Email == supplierEmail;
        }
        public SupplierViewModel SearchSupplier(string searchType, string searchValue)
        {
            IQueryable<Supplier> searchSupplier = _dataContext.Suppliers;

            switch (searchType)
            {
                case "Email":
                    searchSupplier = searchSupplier.Where(s => s.Email.ToUpper().Contains(searchValue.ToUpper()) && s.IsLocked == false);
                    break;

                default:
                    var query = $"SELECT * FROM Suppliers WHERE {searchType} COLLATE NOCASE LIKE '%' || @searchValue || '%' AND IsLocked = 0";
                    searchSupplier = _dataContext.Suppliers.FromSqlRaw(query, new SqliteParameter("@searchValue", searchValue));
                    break;
            }

            if (searchSupplier.Any())
            {
                var searchSupplierDto = _mapper.Map<List<SupplierDTO>>(searchSupplier.ToList());

                foreach (var searchDto in searchSupplierDto)
                {
                    searchDto.Address = ExtractCityFromAddress(searchDto.Address);
                }

                var supplierViewModel = new SupplierViewModel { Suppliers = searchSupplierDto };
                return supplierViewModel;
            }
            return null!;
        }

        public SupplierViewModel SearchSupplierArchive(string searchType, string searchValue)
        {
            IQueryable<Supplier> searchSupplier = _dataContext.Suppliers;

            switch (searchType)
            {
                case "Email":
                    searchSupplier = searchSupplier.Where(s => s.Email.ToUpper().Contains(searchValue.ToUpper()) && s.IsLocked == true);
                    break;

                default:
                    var query = $"SELECT * FROM Suppliers WHERE {searchType} COLLATE NOCASE LIKE '%' || @searchValue || '%' AND IsLocked = 1";
                    searchSupplier = _dataContext.Suppliers.FromSqlRaw(query, new SqliteParameter("@searchValue", searchValue));
                    break;
            }

            if (searchSupplier.Any())
            {
                var searchSupplierDto = _mapper.Map<List<SupplierDTO>>(searchSupplier.ToList());

                foreach (var searchDto in searchSupplierDto)
                {
                    searchDto.Address = ExtractCityFromAddress(searchDto.Address);
                }

                var supplierViewModel = new SupplierViewModel { Suppliers = searchSupplierDto };
                return supplierViewModel;
            }
            return null!;
        }

        public bool Deactive(long supplierId)
        {
            try
            {
                var supplier = _dataContext.Suppliers.FirstOrDefault(u => u.SupplierId == supplierId);
                if (supplier == null)
                {
                    return false;
                }
                supplier.IsLocked = true;
                _dataContext.Entry(supplier).State = EntityState.Modified;
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Active(long supplierId)
        {
            try
            {
                var supplier = _dataContext.Suppliers.FirstOrDefault(u => u.SupplierId == supplierId);
                if (supplier == null)
                {
                    return false;
                }
                supplier.IsLocked = false;
                _dataContext.Entry(supplier).State = EntityState.Modified;
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string ExtractCityFromAddress(string fullAddress)
        {
            string[] addressParts = fullAddress.Split(',');

            int maxIndex = Math.Min(4, addressParts.Length - 1);

            string city = addressParts[maxIndex - 1].Trim();
            return city;
        }
    }
}
