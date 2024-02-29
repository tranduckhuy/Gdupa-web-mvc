﻿using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Utils.Mail;
using WarehouseWebMVC.Utils.Helper;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    private readonly SendMailUtil _sendMailUtil;
    private readonly IResetPasswordHelper _resetPasswordHelper;

    public UserService(DataContext dataContext, IMapper mapper, 
                        SendMailUtil sendMailUtil, IResetPasswordHelper resetPasswordHelper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
        _sendMailUtil = sendMailUtil;
        _resetPasswordHelper = resetPasswordHelper;
    }

    public LoginResult CheckLogin(UserDTO userDTO)
    {
        var user = _dataContext.Users.SingleOrDefault(u => u.Email == userDTO.Email);

        if (user != null)
        {
            if (user.IsLocked)
            {
                return LoginResult.AccountLocked;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password);

            return isValidPassword ? LoginResult.Success : LoginResult.InvalidCredentials;
        }

        return LoginResult.InvalidCredentials;
    }


    public User GetUserByEmail(string email)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email == email)!;
    }

    public UserInformationDTO GetUserById(long userId)
    {
        var user = _dataContext.Users.FirstOrDefault(u => u.UserId == userId)!;
        var userDto = _mapper.Map<UserInformationDTO>(user);
        return userDto;
    }

    public bool UpdateUser(UserInformationDTO updatedUser)
    {
        try
        {
            var existingUser = _dataContext.Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);

            if (existingUser == null)
            {
                return false;
            }

            if (updatedUser.Avatar == null)
            {
                updatedUser.Avatar = existingUser.Avatar;
            }

            if (updatedUser.Name != null && updatedUser.Phone != null && updatedUser.Avatar != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Phone = updatedUser.Phone;
                existingUser.Avatar = updatedUser.Avatar;
            }

            _dataContext.Entry(existingUser).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool ChangePassword(UserInformationDTO updatedUser)
    {
        try
        {
            var existingUser = _dataContext.Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);

            if (existingUser == null
                || updatedUser.OldPassword == null
                || updatedUser.NewPassword == null
                || updatedUser.ConfirmPassword == null
                || !BCrypt.Net.BCrypt.Verify(updatedUser.OldPassword, existingUser.Password)
                || updatedUser.OldPassword == updatedUser.NewPassword
                || updatedUser.NewPassword != updatedUser.ConfirmPassword)
            {
                return false;
            }
            updatedUser.NewPassword = BCrypt.Net.BCrypt.HashPassword(updatedUser.NewPassword);
            existingUser.Password = updatedUser.NewPassword;
            _dataContext.Entry(existingUser).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SendResetPasswordEmail(string userEmail, ISession session, HttpContext httpContext)
    {
        var user = _dataContext.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user != null)
        {
            var resetToken = Guid.NewGuid().ToString();
            var resetTokenExpiryTime = DateTime.UtcNow.AddMinutes(2);

            session.SetString("ResetToken", resetToken);
            session.SetString("ResetTokenExpiryTime", resetTokenExpiryTime.ToString());
            session.SetString("ResetTokenUserEmail", userEmail);

            var resetLink = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/Authentication/ResetPassword?token={resetToken}";
            var mailContent = new MailContent
            {
                To = userEmail,
                Subject = "Reset Password",
                Body = _resetPasswordHelper.RenderBodyResetPassword(resetLink)
            };
            _sendMailUtil.SendMail(mailContent).Wait();

            return true;
        }
        return false;
    }

    public bool ResetPassword(string newPassword, ISession session)
    {
        var sessionToken = session.GetString("ResetToken");
        var sessionExpiryTime = session.GetString("ResetTokenExpiryTime");

        if (string.IsNullOrEmpty(sessionToken) || string.IsNullOrEmpty(sessionExpiryTime) || DateTime.Parse(sessionExpiryTime) <= DateTime.UtcNow)
        {
            return false;
        }

        var userEmailFromSession = session.GetString("ResetTokenUserEmail");

        if (string.IsNullOrEmpty(userEmailFromSession))
        {
            Console.WriteLine("User email from session is null or empty.");
            return false;
        }

        try
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.Email == userEmailFromSession);

            if (user != null)
            {
                newPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.Password = newPassword;

                session.Remove("ResetToken");
                session.Remove("ResetTokenExpiryTime");
                session.Remove("ResetTokenUserEmail");

                _dataContext.SaveChanges();
                return true;
            }
            else
            {
                Console.WriteLine($"User with email '{userEmailFromSession}' not found in the database.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving changes to the database: {ex.Message}");
        }

        return false;
    }

    public UserViewModel GetAll(int page)
    {

        var totalUsers = _dataContext.Users.Count();
        const int pageSize = 6;
        if (page < 1)
        {
            page = 1;
        }
        var pageable = new Pageable(totalUsers, page, pageSize);

        int skipAmount = (pageable.CurrentPage - 1) * pageSize;

        var users = _dataContext.Users
            .Skip(skipAmount)
            .Take(pageSize)
            .Include(p => p.ReceivedExpenseReports)
            .Include(p => p.SentExpenseReports)
            .OrderBy(p => p.UserId)
            .ToList();

        var usersDto = _mapper.Map<List<UserDTO>>(users);

        foreach (var userDto in usersDto)
        {
            userDto.Address = ExtractCityFromAddress(userDto.Address);
        }

        var userViewModel = new UserViewModel { Users = usersDto, Pageable = pageable };

        return userViewModel;
    }

    public UserViewModel SearchUser(string searchType, string searchValue)
    {
        IQueryable<User> searchUser = _dataContext.Users;

        switch (searchType)
        {
            case "Email":
                searchUser = searchUser.Where(u => u.Email.ToUpper().Contains(searchValue.ToUpper()));
                break;

            default:
                var query = $"SELECT * FROM Users WHERE {searchType} COLLATE NOCASE LIKE '%' || @searchValue || '%'";
                searchUser = _dataContext.Users.FromSqlRaw(query, new SqliteParameter("@searchValue", searchValue));
                break;
        }

        if (searchUser.Any())
        {
            var searchUserDto = _mapper.Map<List<UserDTO>>(searchUser.ToList());

            foreach (var searchDto in searchUserDto)
            {
                searchDto.Address = ExtractCityFromAddress(searchDto.Address);
            }

            var userViewModel = new UserViewModel { Users = searchUserDto };
            return userViewModel;
        }
        return null!;
    }

    public bool Deactive(long userId, long inforId)
    {
        try
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null || user.UserId == inforId)
            {
                return false;
            }
            user.IsLocked = true;
            _dataContext.Entry(user).State = EntityState.Modified;
            _dataContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Active(long userId, long inforId)
    {
        try
        {
            var user = _dataContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null || user.UserId == inforId)
            {
                return false;
            }
            user.IsLocked = false;
            _dataContext.Entry(user).State = EntityState.Modified;
            _dataContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsEmailAlreadyExists(string email)
    {
        return _dataContext.Users.Any(u => u.Email == email);
    }

    public bool AddUser(AddUserDTO addUserDTO)
    {
        try
        {
            if (_dataContext.Users.Any(u => u.Email == addUserDTO.Email))
            {
                return false;
            }

            if (addUserDTO.Password != addUserDTO.RepeatPassword)
            {
                return false;
            }

            addUserDTO.Avatar ??= "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
            addUserDTO.Role = "Staff";
            addUserDTO.IsLocked = false;
            addUserDTO.Password = BCrypt.Net.BCrypt.HashPassword(addUserDTO.Password);
            addUserDTO.Address =
                addUserDTO.Street + ", "
                + (addUserDTO.Apartment != null && addUserDTO.Apartment != "" ? addUserDTO.Apartment + ", " : "")
                + (addUserDTO.Ward != null && addUserDTO.Ward != "" ? addUserDTO.Ward + ", " : "")
                + addUserDTO.District + ", "
                + addUserDTO.Province;

            var userEntity = _mapper.Map<User>(addUserDTO);

            userEntity.CreatedAt = DateTime.Now;
            userEntity.Address = addUserDTO.Address;

            _dataContext.Users.Add(userEntity);
            _dataContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UserOwnsInformation(string userEmail, long userId)
    {
        var user = GetUserById(userId);
        return user != null && user.Email == userEmail;
    }

    public long GetUserIdByEmail(string userEmail)
    {
        var user = _dataContext.Users.FirstOrDefault(u => u.Email == userEmail);

        return user?.UserId ?? 0;
    }

    private static string ExtractCityFromAddress(string fullAddress)
    {
        string[] addressParts = fullAddress.Split(',');

        int maxIndex = Math.Min(4, addressParts.Length - 1);

        string city = addressParts[maxIndex - 1].Trim();
        return city;
    }
}

public enum LoginResult
{
    Success,
    InvalidCredentials,
    AccountLocked
}