using AutoMapper;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services.Mail;

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

    public bool CheckLogin(UserDTO userDTO)
    {
        var user = _dataContext.Users.SingleOrDefault(u => u.Email == userDTO.Email);

        return user != null && user.Password == userDTO.Password;
    }

    public User GetUserByEmail(UserDTO userDTO)
    {
        return _dataContext.Users.FirstOrDefault(u => u.Email == userDTO.Email);
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
                Body = $"Click the following link to reset your password: <a href='{resetLink}'>CLICK</a>"
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
}
