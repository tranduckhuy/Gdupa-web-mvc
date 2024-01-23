using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
    public class SupplierViewModel
    {
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
