using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain
{
    public class Warehouse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WarehouseId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ModifiedAt { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
