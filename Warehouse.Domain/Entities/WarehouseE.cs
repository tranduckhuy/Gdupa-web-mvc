using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Domain.Entities
{
    public class WarehouseE
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WarehouseId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int QuantityAtBeginPeriod { get; set; }
        [Required]
        public int QuantityImport { get; set; }
        [Required]
        public double PriceImport { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        // foreign key ProductId
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;

    }

}

