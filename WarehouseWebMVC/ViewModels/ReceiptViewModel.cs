using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
	public class ReceiptViewModel
	{
		public ICollection<Receipt> Receipts { get; set; }	= new List<Receipt>();
		public Pageable Pageable { get; set; } = null!;
	}
}
