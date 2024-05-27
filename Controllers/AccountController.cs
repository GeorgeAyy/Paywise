using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IAppLogger _logger;
    private readonly IUserService _userService;

    public AccountController(IUserService userService, IAppLogger logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        _logger.LogInfo("Login page requested.");
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        _logger.LogInfo("Register page requested.");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        _logger.LogInfo($"Login attempt for username: {username}");
        var user = await _userService.ValidateUserCredentials(username, password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            HttpContext.Session.SetString("UserName", username);
            _logger.LogInfo($"User {username} logged in successfully.");
            return RedirectToAction("Index", "Home");
        }
        else
        {
            _logger.LogError($"Invalid login attempt for username: {username}");
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            _logger.LogError("Passwords do not match during registration.");
            ViewBag.ErrorMessage = "Passwords do not match";
            return View();
        }

        var existingUser = await _userService.FindByUsernameAsync(username);
        if (existingUser != null)
        {
            _logger.LogError($"Registration attempt with existing username: {username}");
            ViewBag.ErrorMessage = "Username already exists";
            return View();
        }

        var newUser = new User
        {
            Username = username,
            Email = email
        };

        await _userService.RegisterUserAsync(newUser, password);
        _logger.LogInfo($"User {username} registered successfully.");
        ViewBag.SuccessMessage = "User registered successfully";
        return RedirectToAction("Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        var username = HttpContext.Session.GetString("UserName");
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInfo($"User {username} logged out.");
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);
        var expenses = await _userService.GetUserExpensesAsync(userId);

        ViewBag.User = user;
        ViewBag.Expenses = expenses;

        _logger.LogInfo($"Profile page requested for user ID: {userId}");
        return View();
    }
}
