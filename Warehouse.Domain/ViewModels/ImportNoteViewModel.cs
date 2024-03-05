using Warehouse.Domain.Entities;

namespace Warehouse.Domain.ViewModels
{
    public class ImportNoteViewModel
    {
        public ICollection<ImportNote> ImportNotes { get; set; } = new List<ImportNote>();
        public Pageable Pageable { get; set; } = null!;
    }
}
