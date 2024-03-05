using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
    public class ImportNoteDetailVM
    {
        public ImportNote ImportNote { get; set; } = null!;
        public ICollection<ImportNoteDetail> ImportNoteDetails { get; set; } = new List<ImportNoteDetail>();
    }
}
