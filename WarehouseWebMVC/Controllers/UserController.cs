using Microsoft.AspNetCore.Mvc;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Utils.Helper;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared.DTOs.UserDTO;
using Warehouse.Shared.ViewModels;
using WarehouseWebMVC.AuthenticationFilter;

namespace WarehouseWebMVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IAddressHelper _addressHelper;

    public UserController(ILogger<UserController> logger,
        IUserService userService,
        IAddressHelper addressHelper)
    {
        _logger = logger;
        _userService = userService;
        _addressHelper = addressHelper;
    }

    [Filter]
    [HttpGet]
    public IActionResult Users(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            UserViewModel userViewModel = _userService.GetAll(page);
            return View(userViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult UserInformation(long userId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var userInformationVM = _userService.GetUserById(userId);
            var currentUserEmail = HttpContext.Session.GetString("User");
            if (currentUserEmail != null && _userService.UserOwnsInformation(currentUserEmail, userId))
            {
                return View(userInformationVM);
            }
            else
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("UserInformation", new { userId = _userService.GetUserIdByEmail(currentUserEmail!) });
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult AddUser()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            return View();
        }
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult AddUser(AddUserDTO addUserDTO)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            ModelState.Remove("Ward");
            ModelState.Remove("Apartment");
            if (_userService.IsEmailAlreadyExists(addUserDTO.Email))
            {
                TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
                return View(addUserDTO);
            }
            if (ModelState.IsValid)
            {
                if (_userService.SendAddUserEmail(addUserDTO.Email, HttpContext.Session, HttpContext))
                {
                    if (_userService.AddUser(addUserDTO))
                    {
                        TempData["Message"] = AppConstant.MESSAGE_SENT_SUCCESSFUL;
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        TempData["Message"] = AppConstant.MESSAGE_FAILED;
                        return View(addUserDTO);
                    }
                }
                else
                {
                    TempData["Message"] = AppConstant.MESSAGE_NULL;
                    return View(addUserDTO);
                }
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult UserInformation(UserInformationDTO userInformationDTO)
    {
        if (ModelState.IsValid)
        {
            var currentUserEmail = HttpContext.Session.GetString("User");

            if (currentUserEmail != null && _userService.UserOwnsInformation(currentUserEmail, userInformationDTO.UserId))
            {
                if (_userService.UpdateUser(userInformationDTO))
                {
                    var user = _userService.GetUserById(userInformationDTO.UserId);
                    if (user != null)
                    {
                        byte[] userIdBytes = BitConverter.GetBytes(user.UserId);
                        HttpContext.Session.Set("Id", userIdBytes);
                        HttpContext.Session.SetString("Name", user.Name);
                        HttpContext.Session.SetString("User", user.Email);
                        HttpContext.Session.SetString("Avatar", user.Avatar);
                        string address = _addressHelper.ExtractCityProvince(user.Address);
                        HttpContext.Session.SetString("Address", address);
                    }
                    else
                    {
                        TempData["Message"] = AppConstant.MESSAGE_FAILED;
                        return View(_userService.GetUserById(userInformationDTO.UserId));
                    }
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return View(_userService.GetUserById(userInformationDTO.UserId));
                }
            }
            else
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("UserInformation", new { userId = _userService.GetUserIdByEmail(currentUserEmail!) });
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return View(_userService.GetUserById(userInformationDTO.UserId));
    }

    [HttpPost]
    public IActionResult ChangePassword(UserInformationDTO userInformationDTO)
    {
        var user = _userService.GetUserById(userInformationDTO.UserId);
        if (ModelState.IsValid)
        {
            var currentUserEmail = HttpContext.Session.GetString("User");

            if (currentUserEmail != null && _userService.UserOwnsInformation(currentUserEmail, userInformationDTO.UserId))
            {
                if (_userService.ChangePassword(userInformationDTO))
                {
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return View("UserInformation", user);
                }
                else
                {
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return View("UserInformation", user);
                }
            }
            else
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("UserInformation", new { userId = _userService.GetUserIdByEmail(currentUserEmail!) });
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return View("UserInformation", user);
    }

    [HttpGet]
    public IActionResult DeactiveUser(long userId, long inforId)
    {
        if (_userService.Deactive(userId, inforId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Users");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Users");
    }

    [HttpGet]
    public IActionResult ActiveUser(long userId, long inforId)
    {
        if (_userService.Active(userId, inforId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Users");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Users");
    }

    [HttpGet]
    public IActionResult ActiveByEmail(string email, string expiryTime)
    {
        if (_userService.ActiveByEmail(email, expiryTime))
        {
            TempData["Message"] = AppConstant.MESSAGE_ACTIVE_SUCCESSFUL;
            return RedirectToAction("Login", "Authentication");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult SearchUser(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchUsers = _userService.SearchUser(searchType, searchValue);
            if (searchUsers != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                return View("Users", searchUsers);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("Users");
    }
}