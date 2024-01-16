using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.Service;

public interface IUserService
{
    public bool CheckLogin(UserDTO userDTO);
}
