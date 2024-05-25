using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Threading.Tasks;

namespace MyMvcApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpenseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expenses.Find(_ => true).ToListAsync();
            return View(expenses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _context.Expenses.InsertOneAsync(expense);
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }
    }
}
