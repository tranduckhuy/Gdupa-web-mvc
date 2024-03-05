using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Utils.Mail;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class ContactController : Controller
{
    private readonly SendMailUtil _sendMailUtil;
    private readonly MailSettings _mailSettings;

    public ContactController(SendMailUtil sendMailUtil, IOptions<MailSettings> mailSettings)
    {
        _sendMailUtil = sendMailUtil;
        _mailSettings = mailSettings.Value;
    }

    [HttpPost]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            var mailContent = new MailContent
            {
                To = _mailSettings.Email,
                Subject = model.Subject,
                Body = $"You have a new contact request from {model.Name} ({model.Email}). Message: {model.Message}"
            };

            var result = await _sendMailUtil.SendMail(mailContent);

            if (result.StartsWith("Error"))     
            {
                TempData["Message"] = result;
                return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(string email)
    {
        if (ModelState.IsValid)
        {
            var mailContent = new MailContent
            {
                To = _mailSettings.Email,
                Subject = "New Subscribe",
                Body = $"You have a new subscribe from {email}."
            };

            var result = await _sendMailUtil.SendMail(mailContent);

            if (result.StartsWith("Error"))
            {
                TempData["ErrorMessage"] = result;
                return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Index", "Home");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Index", "Home");
    }
}

