using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMongoCollection<User> _users;
     private readonly IMongoCollection<Expense> _expenses;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountController(IMongoClient client,ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        var database = client.GetDatabase("Paywise"); 
        _users = database.GetCollection<User>("Users");
        _expenses = database.GetCollection<Expense>("Expenses");
        _context = context;
        _passwordHasher = passwordHasher;

    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (user != null && _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username)
        };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            HttpContext.Session.SetString("UserName", username);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match";
            return View();
        }

        var existingUser = await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            ViewBag.ErrorMessage = "Username already exists";
            return View();
        }

        var newUser = new User
        {
            Username = username,
            Email = email
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);

        await _context.Users.InsertOneAsync(newUser);
        ViewBag.SuccessMessage = "User registered successfully";
        return RedirectToAction("Login");

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }


   [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        var expenses = await _expenses.Find(e => e.UserId == userId).ToListAsync();

        ViewBag.User = user;
        ViewBag.Expenses = expenses;

        return View();
    }

}
