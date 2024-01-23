using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class ReceiptDetail
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ReceiptDetailId { get; set; }
    [Required]
    public double ImportPrice { get; set; }
    [Required]
    public int Quantity { get; set; }

    // foreign key ReceiptId
    public long ReceiptId { get; set; }
    public Receipt Receipt { get; set; } = null!;

    // foreign key ProductId
    public long ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
