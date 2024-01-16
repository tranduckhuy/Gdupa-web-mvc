using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;

namespace WarehouseWebMVC.ViewModels
{
    public class CRUProductVM
    {
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public ICollection<Category> Categorys { get; set; } = new List<Category>();
        public ICollection<Brand> Brands { get; set; } = new List<Brand>();
        public CURProductDTO Product { get; set; } = null!;
    }
}
