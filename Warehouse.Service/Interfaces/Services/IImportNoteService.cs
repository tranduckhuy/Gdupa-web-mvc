using Warehouse.Shared.DTOs;
using Warehouse.Shared.ViewModels;

namespace Warehouse.Service.Interfaces.Services
{
    public interface IImportNoteService
    {
        bool Add(ImportProductsDTO importProducts);
        ImportNoteViewModel GetAll(int page);
        ImportNoteDetailVM GetDetailById(long importNoteId);
        ImportNoteViewModel SearchImportNote(string searchType, string searchValue);
    }
}
