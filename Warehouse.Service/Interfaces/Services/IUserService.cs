using Microsoft.AspNetCore.Http;
using Warehouse.Shared.DTOs.UserDTO;
using Warehouse.Domain.Entities;
using Warehouse.Shared.ViewModels;

namespace Warehouse.Service.Interfaces.Services
{
    public interface IUserService
    {
        LoginResult CheckLogin(UserDTO userDTO);

        User GetUserByEmail(string email);
        UserInformationDTO GetUserById(long userId);

        bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext);

        bool SendAddUserEmail(string userEmail, ISession session, HttpContext httpContext);

        bool ResetPassword(string newPassword, ISession session);

        bool UpdateUser(UserInformationDTO userDTO);

        bool ChangePassword(UserInformationDTO updatedUser);

        bool Deactive(long id, long inforId);

        bool Active(long id, long inforId);

        bool ActiveByEmail(string email, string expiryTime);

        UserViewModel GetAll(int page);

        UserViewModel SearchUser(string searchType, string searchValue);

        public bool AddUser(AddUserDTO newUser);

        bool UserOwnsInformation(string userEmail, long userId);

        long GetUserIdByEmail(string userEmail);

        bool IsEmailAlreadyExists(string email);

    }

    public enum LoginResult
    {
        Success,
        InvalidCredentials,
        AccountLocked
    }
}

