using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
    public class WarehouseViewModel
    {
        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
        public int LowAlert { get; set; }
        public int OutOfStock { get; set; }
        public Pageable Pageable { get; set; } = null!;
    }
}
