using Warehouse.Domain.Entities;

namespace Warehouse.Domain.ViewModels
{
    public class WarehouseViewModel
    {
        public ICollection<WarehouseE> Warehouses { get; set; } = new List<WarehouseE>();
        public int LowAlert { get; set; }
        public int OutOfStock { get; set; }
        public Pageable Pageable { get; set; } = null!;
        public int Quarter { get; set; }
        public int Year { get; set; }
        public ICollection<int> ImportYears { get; set; } = new List<int>();
        public string Title { get; set; } = null!;
    }
}
