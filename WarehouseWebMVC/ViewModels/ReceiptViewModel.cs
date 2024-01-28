using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.ViewModels
{
	public class ReceiptViewModel
	{
		public ICollection<Receipt> Receipts { get; set; }	= new List<Receipt>();
		public Pageable Pageable { get; set; } = null!;
	}
}
