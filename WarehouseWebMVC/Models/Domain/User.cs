using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        public ICollection<Invoice> Invoices { get; } = new List<Invoice>();
        public ICollection<ExpenseReport> SentExpenseReports { get; set; } = new List<ExpenseReport>();
        public ICollection<ExpenseReport> ReceivedExpenseReports { get; set; } = new List<ExpenseReport>();
    }
}
