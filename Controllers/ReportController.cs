using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMvcApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IMongoCollection<Expense> _expenses;

        public ReportController(IMongoClient client)
        {
            var database = client.GetDatabase("Paywise");
            _expenses = database.GetCollection<Expense>("Expenses");
        }

        public IActionResult Generate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _expenses.Find(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate).ToListAsync();

            var reportViewModel = new Report
            {
                StartDate = startDate,
                EndDate = endDate,
                Expenses = expenses
            };

            return View("Report", reportViewModel);
        }
    }
}
