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
