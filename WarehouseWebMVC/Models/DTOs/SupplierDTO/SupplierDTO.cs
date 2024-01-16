using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs.SupplierDTO
{
    public class SupplierDTO
    {
        public long SupplierId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
