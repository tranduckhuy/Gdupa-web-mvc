using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
	public interface IReceiptService
	{
		bool Add(ImportProductsDTO importProducts);
		ReceiptViewModel GetAll(int page);
	}
}
