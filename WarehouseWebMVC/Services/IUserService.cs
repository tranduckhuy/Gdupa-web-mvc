using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Service;

public interface IUserService
{
    public bool CheckLogin(UserDTO userDTO);

    public User GetUserByEmail(string email);

    public UserInformationVM GetUserById(long userId);

    public bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext);

    public bool ResetPassword(string newPassword, ISession session);

    public UserViewModel GetAll(int page);
}
