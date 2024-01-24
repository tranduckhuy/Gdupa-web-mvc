using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs
{
    public class ImportProductsDTO
    {
        public ICollection<Warehouse> ImportProducts { get; set; } = new List<Warehouse>();
        public long UserId { get; set; }
        public long SupplierId { get; set; }
        public long Total { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ReasonDetail { get; set; } = string.Empty;
        public string Deliverer { get; set; } = string.Empty;
    }
}
