using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class Receipt
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ReceiptId { get; set; }
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

    public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
