﻿using Microsoft.EntityFrameworkCore;
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

		public ReceiptSerivce(DataContext dataContext)
		{
			_dataContext = dataContext;
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
	}
}