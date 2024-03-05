using Warehouse.Domain.DTOs;
using Warehouse.Domain.ViewModels;

namespace Warehouse.Domain.Interfaces
{
    public interface IImportNoteService
    {
        bool Add(ImportProductsDTO importProducts);
        ImportNoteViewModel GetAll(int page);
        ImportNoteDetailVM GetDetailById(long importNoteId);
        ImportNoteViewModel SearchImportNote(string searchType, string searchValue);
    }
}
