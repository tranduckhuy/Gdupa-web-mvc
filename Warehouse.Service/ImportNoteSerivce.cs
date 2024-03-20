using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Warehouse.Domain;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Data;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared;
using Warehouse.Shared.DTOs;
using Warehouse.Shared.ViewModels;

namespace WarehouseWebMVC.Services
{
    public class ImportNoteSerivce : IImportNoteService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ImportNoteSerivce(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public bool Add(ImportProductsDTO importProducts)
        {
            try
            {
                using (var transaction = _dataContext.Database.BeginTransaction())
                {
                    try
                    {
                        var importNote = new ImportNote
                        {
                            Total = importProducts.Total,
                            Deliverer = importProducts.Deliverer,
                            Reason = importProducts.Reason,
                            ReasonDetail = importProducts.ReasonDetail,
                            UserId = importProducts.UserId,
                            SupplierId = importProducts.SupplierId,
                            CreatedAt = DateTime.UtcNow.ToLocalTime()
                        };

                        _dataContext.ImportNotes.Add(importNote);
                        _dataContext.SaveChanges();

                        var ImportNoteId = importNote.ImportNoteId;

                        foreach (var product in importProducts.ImportProducts)
                        {
                            _dataContext.ImportNoteDetails.Add(new ImportNoteDetail
                            {
                                ImportNoteId = ImportNoteId,
                                ProductId = product.ProductId,
                                ImportPrice = product.PriceImport,
                                Quantity = product.Quantity
                            });
                        }

                        _dataContext.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ImportNoteViewModel GetAll(int page)
        {
            var total = _dataContext.ImportNotes.Count();

            const int pageSize = 5;

            if (page < 1)
            {
                page = 1;
            }
            var pageable = new Pageable(total, page, pageSize);

            int skipAmount = (pageable.CurrentPage - 1) * pageSize;

            var importNotes = _dataContext.ImportNotes
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(u => u.User)
                .Include(s => s.Supplier)
                .OrderBy(r => r.ImportNoteId)
                .ToList();

            var importNotesViewModel = new ImportNoteViewModel { ImportNotes = importNotes, Pageable = pageable };

            return importNotesViewModel;
        }

        public ImportNoteDetailVM GetDetailById(long importNoteId)
        {
            var importNote = _dataContext.ImportNotes
                .Include(u => u.User)
                .Include(s => s.Supplier)
                .FirstOrDefault(r => r.ImportNoteId == importNoteId);

            if (importNote != null)
            {
                var importNoteDetail = _dataContext.ImportNoteDetails
                    .Where(r => r.ImportNoteId == importNote.ImportNoteId)
                    .Include(p => p.Product)
                    .ToList();
                return new ImportNoteDetailVM { ImportNote = importNote, ImportNoteDetails = importNoteDetail };
            }
            return null!;
        }

        public ImportNoteViewModel SearchImportNote(string searchType, string searchValue)
        {
            IQueryable<ImportNote> searchImportNote = _dataContext.ImportNotes
                .Include(i => i.User)
                .Include(i => i.Supplier);

            switch (searchType)
            {
                case "Supplier":
                    searchImportNote = searchImportNote.Where(s => s.Supplier.Name.ToUpper().Contains(searchValue.ToUpper()));
                    break;
                case "Deliverer":
                    searchImportNote = searchImportNote.Where(s => s.Deliverer.ToUpper().Contains(searchValue.ToUpper()));
                    break;
                default:
                    searchImportNote = searchImportNote.Where(s => s.User.Name.ToUpper().Contains(searchValue.ToUpper()))
                        .Include(i => i.User)
                        .Include(i => i.Supplier);
                    break;
            }

            if (searchImportNote.Any())
            {
                var searchImportNotes = _mapper.Map<List<ImportNote>>(searchImportNote.ToList());
                var importNoteViewModel = new ImportNoteViewModel { ImportNotes = searchImportNotes };
                return importNoteViewModel;
            }
            return null!;
        }
    }
}
