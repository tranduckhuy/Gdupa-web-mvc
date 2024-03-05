using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs
{
    public class ImportNoteDTO
    {
        public long ImportNoteId { get; set; }
        public double Total { get; set; }
        public string Deliverer { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string ReasonDetail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public ICollection<ImportNoteDetail> ImportNoteDetails { get; set; } = new List<ImportNoteDetail>();
    }
}
