using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MyMvcApp.Models;
using MyMvcApp.Services;

namespace MyMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IAppLogger _logger;

        public HomeController(IServiceFactory serviceFactory, IAppLogger logger)
        {
            _serviceFactory = serviceFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Retrieve the authenticated user's ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInfo($"Home page requested by user ID: {userId}");


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
            _logger.LogInfo("Privacy page requested.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error page requested.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
