using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs.UserDTO;

public class AddUserDTO
{
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string RepeatPassword { get; set; } = string.Empty;

    public string Avatar { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";

    public DateTime CreatedAt { get; set; }

    public ICollection<Invoice> Invoices { get; } = new List<Invoice>();
    public ICollection<ExpenseReport> SentExpenseReports { get; set; } = new List<ExpenseReport>();
    public ICollection<ExpenseReport> ReceivedExpenseReports { get; set; } = new List<ExpenseReport>();
}
