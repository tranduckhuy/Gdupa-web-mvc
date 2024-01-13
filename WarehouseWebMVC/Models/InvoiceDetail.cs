using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models
{
    public class InvoiceDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceDetailId { get; set; }
        [Required]
        public double ImportPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        // foreign key invoiceId
        public long InvoiceId { get; set; }
        public Invoice Invoice { get; set; } = null!;

        // foreign key ProductId
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
