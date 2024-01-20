using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Services.Mail;

public class SendMailService
{
    MailSettings _mailSettings { get; set; }

    public SendMailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task<string> SendMail(MailContent mailContent)
    {
        var email = new MimeMessage();
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email);
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
        email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
        email.Subject = mailContent.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = mailContent.Body;

        email.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return "Error " + e.Message;
        }

        smtp.Disconnect(true);
        return "SEND SUCCESSFULLY";
    }
}


public class MailContent
{
    public string To { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;
}