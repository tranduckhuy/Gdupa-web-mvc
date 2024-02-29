using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WarehouseWebMVC.Utils.Mail;
public class SendMailUtil
{
    private MailSettings MailSettings { get; set; }

    public SendMailUtil(IOptions<MailSettings> mailSettings)
    {
        MailSettings = mailSettings.Value;
    }

    public async Task<string> SendMail(MailContent mailContent)
    {
        var email = new MimeMessage();
        email.Sender = new MailboxAddress(MailSettings.DisplayName, MailSettings.Email);
        email.From.Add(new MailboxAddress(MailSettings.DisplayName, MailSettings.Email));
        email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
        email.Subject = mailContent.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = mailContent.Body;

        email.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            await smtp.ConnectAsync(MailSettings.Host, MailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(MailSettings.Email, MailSettings.Password);
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