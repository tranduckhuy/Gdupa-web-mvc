using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl
{
    public class ReceiptSerivce : IReceiptService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ReceiptSerivce(DataContext dataContext, IMapper mapper)
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
                        var receipt = new Receipt
                        {
                            Total = importProducts.Total,
                            Deliverer = importProducts.Deliverer,
                            Reason = importProducts.Reason,
                            ReasonDetail = importProducts.ReasonDetail,
                            UserId = importProducts.UserId,
                            SupplierId = importProducts.SupplierId,
                        };

                        _dataContext.Receipts.Add(receipt);
                        _dataContext.SaveChanges();

                        var receiptId = receipt.ReceiptId;

                        foreach (var product in importProducts.ImportProducts)
                        {
                            _dataContext.ReceiptDetails.Add(new ReceiptDetail
                            {
                                ReceiptId = receiptId,
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

        public ReceiptViewModel GetAll(int page)
        {
            var total = _dataContext.Receipts.Count();

            const int pageSize = 5;

            if (page < 1)
            {
                page = 1;
            }
            var pageable = new Pageable(total, page, pageSize);

            int skipAmount = (pageable.CurrentPage - 1) * pageSize;

            var receipts = _dataContext.Receipts
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(u => u.User)
                .Include(s => s.Supplier)
                .OrderBy(r => r.ReceiptId)
                .ToList();

            var receiptsViewModel = new ReceiptViewModel { Receipts = receipts, Pageable = pageable };

            return receiptsViewModel;
        }

        public ReceiptDetailVM GetDetailById(long receiptId)
        {
            var receipt = _dataContext.Receipts
                .Include(u => u.User)
                .Include(s => s.Supplier)
                .FirstOrDefault(r => r.ReceiptId == receiptId);

            if (receipt != null)
            {
                var receiptDetail = _dataContext.ReceiptDetails
                    .Where(r => r.ReceiptId == receipt.ReceiptId)
                    .Include(p => p.Product)
                    .ToList();
                return new ReceiptDetailVM { Receipt = receipt, ReceiptDetails = receiptDetail };
            }
            return null!;
        }

        public ReceiptViewModel SearchReceipt(string searchType, string searchValue)
        {
            IQueryable<Receipt> searchReceipt = _dataContext.Receipts
                .Include(u => u.User)
                .Include(s => s.Supplier);

            switch (searchType)
            {
                case "Supplier":
                    searchReceipt = searchReceipt.Where(r => r.Supplier.Name.ToUpper().Contains(searchValue.ToUpper()));
                    break;
                case "Deliverer":
                    searchReceipt = searchReceipt.Where(r => EF.Functions.Collate(r.Deliverer, "NOCASE").Contains(searchValue));
                    break;
                default:
                    searchReceipt = searchReceipt.Where(r => EF.Functions.Collate(r.User.Name, "NOCASE").Contains(searchValue));
                    break;
            }

            if (searchReceipt.Any())
            {
                var searchReceipts = _mapper.Map<List<Receipt>>(searchReceipt.ToList());
                var receiptViewModel = new ReceiptViewModel { Receipts = searchReceipts };
                return receiptViewModel;
            }
            return null!;
        }
    }
}
