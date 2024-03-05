using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Domain.DTOs.ProductDTO;
using Warehouse.Domain.Entities;

namespace Warehouse.Domain.ViewModels
{
    public class CRUProductVM
    {
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Brand> Brands { get; set; } = new List<Brand>();
        public ICollection<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public AddProductDTO Product { get; set; } = null!;
    }
}
