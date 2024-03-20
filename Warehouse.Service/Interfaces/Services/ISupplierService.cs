﻿using Warehouse.Shared.DTOs;
using Warehouse.Shared.ViewModels;

namespace Warehouse.Service.Interfaces.Services
{
    public interface ISupplierService
    {
        SupplierDTO GetById(long supplierId);
        SupplierViewModel GetLimit(int page, bool archive);
        SupplierViewModel SearchSupplier(string searchType, string searchValue, bool archive);
        bool AddSupplier(SupplierDTO addSupplierDTO);
        bool UpdateSupplier(SupplierDTO updateSupplierDTO);
        bool IsEmailAlreadyExists(string email);
        bool SupplierOwnsInformation(string supplierEmail, long supplierId);
        bool Deactive(long supplierId);
        bool Active(long supplierId);
        int CountSupplierNotArchived();
        int CountSupplierArchived();
    }
}
