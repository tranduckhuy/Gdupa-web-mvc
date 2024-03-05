using Warehouse.Domain.Entities;

namespace Warehouse.Domain.DTOs.UserDTO;

public class UserDTO
{
    public long UserId { get; set; }
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;
    public bool IsLocked { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<ExpenseReport> SentExpenseReports { get; set; } = new List<ExpenseReport>();
    public ICollection<ExpenseReport> ReceivedExpenseReports { get; set; } = new List<ExpenseReport>();

    public string ResetToken { get; set; } = string.Empty;

    public DateTime? ResetTokenExpiryTime { get; set; }

    public string NewPassword { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}
