using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.ViewModels
{
	public class ImportNoteViewModel
	{
		public ICollection<ImportNote> ImportNotes { get; set; }	= new List<ImportNote>();
		public Pageable Pageable { get; set; } = null!;
	}
}
