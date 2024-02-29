using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
	public class ImportNoteViewModel
	{
		public ICollection<ImportNote> ImportNotes { get; set; }	= new List<ImportNote>();
		public Pageable Pageable { get; set; } = null!;
	}
}
