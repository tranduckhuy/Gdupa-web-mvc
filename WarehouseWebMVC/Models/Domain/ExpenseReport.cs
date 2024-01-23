using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class ExpenseReport
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ExpenseReportId { get; set; }
    [Required]
    [Column(TypeName = "TEXT")]
    public string Reason { get; set; } = string.Empty;
    [Required]
    public double Total { get; set; }
    [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    public long SenderId { get; set; }
    public long ReceiverId { get; set; }
    public User Sender { get; set; } = null!;
    public User Receiver { get; set; } = null!;
}
