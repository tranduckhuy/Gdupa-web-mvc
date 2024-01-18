using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;

namespace WarehouseWebMVC.Service;

public interface IUserService
{
    public bool CheckLogin(UserDTO userDTO);

    public User GetUserByEmail(UserDTO userDTO);

    public bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext);

    public bool ResetPassword(string newPassword, ISession session);
}
