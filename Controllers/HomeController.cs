using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly IServiceFactory _serviceFactory;

    public HomeController(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<IActionResult> Index()
    {
        // Retrieve the authenticated user's ID from the claims
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Ensure the userId is a valid ObjectId
        if (!ObjectId.TryParse(userId, out var objectId))
        {
            // Handle the case where the userId is not a valid ObjectId
            return BadRequest("Invalid user ID format.");
        }

        var categoryService = _serviceFactory.CreateService<ICategoryService>();
        var expenseService = _serviceFactory.CreateService<IExpenseService>();

        // Example usage of the services with the validated userId
        var categories = await categoryService.GetCategoriesForUserAsync(userId);
        var expenses = await expenseService.GetExpensesForUserAsync(userId);

        ViewBag.Categories = categories;
        ViewBag.Expenses = expenses;

        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
