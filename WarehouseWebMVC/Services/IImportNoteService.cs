using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IImportNoteService
	{
		bool Add(ImportProductsDTO importProducts);
		ImportNoteViewModel GetAll(int page);
        ImportNoteDetailVM GetDetailById(long importNoteId);
        ImportNoteViewModel SearchImportNote(string searchType, string searchValue);
    }
}
