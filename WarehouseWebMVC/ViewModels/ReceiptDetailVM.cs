using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.ViewModels
{
    public class ReceiptDetailVM
    {
        public Receipt Receipt { get; set; } = null!;
        public ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
    }
}
