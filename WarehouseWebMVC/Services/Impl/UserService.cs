﻿using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services.Mail;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    private readonly SendMailService _sendMailService;

    public UserService(DataContext dataContext, IMapper mapper, SendMailService sendMailService)
    {
        _dataContext = dataContext;
        _mapper = mapper;
        _sendMailService = sendMailService;
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
                Body = $@"
        <!doctype html>
        <html lang='en-US'>

        <head>
            <meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
            <title>Reset Password</title>
            <meta name='description' content='Reset Password Email Template.'>
            <style type='text/css'>
                a:hover {{text-decoration: underline !important;}}
            </style>
        </head>

        <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>
            <!--100% body table-->
            <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8'
                style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'>
                <tr>
                    <td>
                        <table style='background-color: #f2f3f8; max-width:670px;  margin:0 auto;' width='100%' border='0'
                            align='center' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td style='height:80px;'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style='text-align:center;'>
                                  <a href='https://localhost:7051/' title='logo' target='_blank'>
                                    <img width='200' src='https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/logo%2Fgdupa-high-resolution-logo-transparent.png?alt=media&token=c438141e-e081-48e3-8b9d-b270bd160fde' title='logo' alt='logo'>
                                  </a>
                                </td>
                            </tr>
                            <tr>
                                <td style='height:20px;'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'
                                        style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>
                                        <tr>
                                            <td style='height:40px;'>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style='padding:0 35px;'>
                                                <h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>You have
                                                    requested to reset your password</h1>
                                                <span
                                                    style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span>
                                                <p style='color:#455056; font-size:15px;line-height:24px; margin:0;'>
                                                    We cannot simply send you your old password. A unique link to reset your
                                                    password has been generated for you. To reset your password, click the
                                                    following link and follow the instructions.
                                                </p>
                                                <a href='{resetLink}'
                                                    style='background:#87CEFA;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>Reset
                                                    Password</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='height:40px;'>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            <tr>
                                <td style='height:20px;'>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style='height:80px;'>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <!--/100% body table-->
        </body>

        </html>"
            };


            _sendMailService.SendMail(mailContent).Wait();


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

            addUserDTO.Password = BCrypt.Net.BCrypt.HashPassword(addUserDTO.Password);
            addUserDTO.Address =
                  (addUserDTO.Apartment != null && addUserDTO.Apartment != "" ? addUserDTO.Apartment + ", " : "")
                + addUserDTO.Street + ", "
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