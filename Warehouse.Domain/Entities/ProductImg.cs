using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities
{
    public class ProductImg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ImageId { get; set; }
        [Required]
        public string ImageURL { get; set; } = string.Empty;

        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}


