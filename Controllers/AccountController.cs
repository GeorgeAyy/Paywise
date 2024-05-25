using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
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
    public IActionResult Login(string username, string password)
    {
        // For simplicity, we'll use hardcoded values.
        // Replace this with your actual user validation logic.
        if (username == "admin" && password == "password")
        {
            // Set session values
            HttpContext.Session.SetString("UserName", username);

            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Show an error message (improve this as needed)
            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }

    [HttpPost]
    public IActionResult Register(string username, string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match";
            return View();
        }

        // For simplicity, we'll use hardcoded validation and user creation.
        // Replace this with your actual user creation logic, such as saving to a database.
        if (username == "admin") // Dummy condition to simulate an existing user
        {
            ViewBag.ErrorMessage = "Username already exists";
            return View();
        }

        // Simulate successful registration
        ViewBag.SuccessMessage = "User registered successfully";

        return View();
    }
}
