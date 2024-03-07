using Warehouse.Domain.Entities;

namespace Warehouse.Shared.DTOs
{
    public class ImportProductsDTO
    {
        public ICollection<WarehouseE> ImportProducts { get; set; } = new List<WarehouseE>();
        public long UserId { get; set; }
        public long SupplierId { get; set; }
        public double Total { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ReasonDetail { get; set; } = string.Empty;
        public string Deliverer { get; set; } = string.Empty;
    }
}
