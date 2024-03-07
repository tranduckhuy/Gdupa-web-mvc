namespace Warehouse.Shared.DTOs
{
    public class SupplierDTO
    {

        public long SupplierId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Avatar { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";

        public string Street { get; set; } = string.Empty;

        public string Apartment { get; set; } = string.Empty;

        public string Province { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string Ward { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Fax { get; set; } = string.Empty;

        public bool IsLocked { get; set; } = false;

        public string Background { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/supplier-background%2Fprofile-cover.jpg?alt=media&token=cf51dca2-8021-40ee-bd58-66000ab49c10";
    }
}
