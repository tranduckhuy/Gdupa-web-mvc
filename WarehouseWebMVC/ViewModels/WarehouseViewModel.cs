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
        public int Quarter { get; set; }
        public int Year { get; set; }
        public ICollection<int> ImportYears { get; set; } = new List<int>();
        public string Title { get; set; } = null!;
    }
}
