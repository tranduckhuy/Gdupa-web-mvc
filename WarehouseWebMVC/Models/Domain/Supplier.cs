using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class Supplier
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SupplierId { get; set; }
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
    public string Fax { get; set; } = string.Empty;

    public ICollection<ImportNote> ImportNotes { get; } = new List<ImportNote>();
}
