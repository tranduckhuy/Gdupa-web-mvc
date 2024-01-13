using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "TEXT")]
        public string Description { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty;

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime ModifiedAt { get; set; }

        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<ProductImg> ProductImgs { get; set; } = new List<ProductImg>();
        public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();


    }
}
