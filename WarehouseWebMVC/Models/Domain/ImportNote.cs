using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class ImportNote
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ImportNoteId { get; set; }
    [Required]
    public double Total { get; set; }
    [Required]
    public string Deliverer { get; set; } = string.Empty;
    [Required]
    public string Reason { get; set; } = string.Empty;
    [Required]
    public string ReasonDetail { get; set; } = string.Empty;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;

    public long SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public ICollection<ImportNoteDetail> ImportNoteDetails { get; set; } = new List<ImportNoteDetail>();
}
