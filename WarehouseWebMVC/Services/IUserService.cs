using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Service;

public interface IUserService
{
    public LoginResult CheckLogin(UserDTO userDTO);

    User GetUserByEmail(string email);

    UserInformationDTO GetUserById(long userId);

    bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext);

    bool ResetPassword(string newPassword, ISession session);

    bool UpdateUser(UserInformationDTO userDTO);

    bool ChangePassword(UserInformationDTO updatedUser);

    bool Deactive(long id, long inforId);

    bool Active(long id, long inforId);

    UserViewModel GetAll(int page);

    UserViewModel SearchUser(string searchType, string searchValue);

    public bool AddUser(AddUserDTO newUser);

    bool UserOwnsInformation(string userEmail, long userId);

    long GetUserIdByEmail(string userEmail);



}