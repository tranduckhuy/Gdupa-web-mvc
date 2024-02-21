using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs.SupplierDTO
{
    public class SupplierDTO
    {
        
        public long SupplierId { get; set; }
   
        public string Name { get; set; } = string.Empty;
       
        public string Email { get; set; } = string.Empty;

        public string Avatar { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
        
        public string Address { get; set; } = string.Empty;
       
        public string Phone { get; set; } = string.Empty;

        public string Fax { get; set; } = string.Empty;
    }
}
