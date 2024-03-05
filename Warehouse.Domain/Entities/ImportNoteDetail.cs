using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Domain.Entities
{
    public class ImportNoteDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ImportNoteDetailId { get; set; }
        [Required]
        public double ImportPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        // foreign key ImportNoteId
        public long ImportNoteId { get; set; }
        public ImportNote ImportNote { get; set; } = null!;

        // foreign key ProductId
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }

}

