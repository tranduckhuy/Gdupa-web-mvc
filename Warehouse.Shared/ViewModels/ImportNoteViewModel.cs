using Warehouse.Domain.Entities;

namespace Warehouse.Shared.ViewModels
{
    public class ImportNoteViewModel
    {
        public ICollection<ImportNote> ImportNotes { get; set; } = new List<ImportNote>();
        public Pageable Pageable { get; set; } = null!;
    }
}
