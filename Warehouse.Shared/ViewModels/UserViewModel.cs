using Warehouse.Shared.DTOs.UserDTO;

namespace Warehouse.Shared.ViewModels
{
    public class UserViewModel
    {
        public ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
        public Pageable Pageable { get; set; } = null!;
    }
}
