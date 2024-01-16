using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.DTOs;

public class UserDTO
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
