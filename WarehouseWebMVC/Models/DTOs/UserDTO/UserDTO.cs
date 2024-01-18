namespace WarehouseWebMVC.Models.DTOs.UserDTO;

public class UserDTO
{
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpiryTime { get; set; }

    public string NewPassword { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;
}
