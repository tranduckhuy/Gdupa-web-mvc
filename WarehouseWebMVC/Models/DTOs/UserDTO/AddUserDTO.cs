using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs.UserDTO;

public class AddUserDTO
{
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string Apartment { get; set; } = string.Empty;

    public string Province { get; set; } = string.Empty;

    public string District { get; set; } = string.Empty;

    public string Ward { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public bool IsLocked { get; set; }

    public string RepeatPassword { get; set; } = string.Empty;

    public string Avatar { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";


}
