using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Service;

public interface IUserService
{
    bool CheckLogin(UserDTO userDTO);

    User GetUserByEmail(string email);

    UserInformationDTO GetUserById(long userId);

    bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext);

    bool ResetPassword(string newPassword, ISession session);

    bool UpdateUser(UserInformationDTO userDTO);

    UserViewModel GetAll(int page);

}