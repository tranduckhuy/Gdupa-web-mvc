using Warehouse.Domain.DTOs.UserDTO;

namespace Warehouse.Domain.ViewModels;

public class UserViewModel
{
    public ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
    public Pageable Pageable { get; set; } = null!;
}
