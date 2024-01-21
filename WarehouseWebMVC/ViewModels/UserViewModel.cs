using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.UserDTO;

namespace WarehouseWebMVC.ViewModels;

public class UserViewModel
{
    public ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
    public Pageable Pageable { get; set; } = null!;
}
