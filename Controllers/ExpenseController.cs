using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.ViewModels;
using System.Security.Claims;

[Authorize]
public class ExpenseController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExpenseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var expenses = await _context.GetExpensesForUserAsync(userId);
        var categories = await _context.GetCategoriesForUserAsync(userId);

        var expenseViewModels = expenses.Select(expense => new ExpenseViewModel
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryName = categories.FirstOrDefault(c => c.Id == expense.CategoryId)?.Name
        }).ToList();

        return View(expenseViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var categories = await _context.GetCategoriesForUserAsync(userId);
        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Expense expense)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        expense.UserId = userId;
        await _context.Expenses.InsertOneAsync(expense);
        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> ViewExpenses(DateTime? minDate, DateTime? maxDate, decimal? minAmount, decimal? maxAmount, string categoryId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var expenses = await _context.GetExpensesForUserAsync(userId);
        var categories = await _context.GetCategoriesForUserAsync(userId);

        if (minDate.HasValue)
        {
            expenses = expenses.Where(e => e.Date >= minDate.Value).ToList();
        }

        if (maxDate.HasValue)
        {
            expenses = expenses.Where(e => e.Date <= maxDate.Value).ToList();
        }

        if (minAmount.HasValue)
        {
            expenses = expenses.Where(e => e.Amount >= minAmount.Value).ToList();
        }

        if (maxAmount.HasValue)
        {
            expenses = expenses.Where(e => e.Amount <= maxAmount.Value).ToList();
        }

        if (!string.IsNullOrEmpty(categoryId))
        {
            expenses = expenses.Where(e => e.CategoryId == categoryId).ToList();
        }

        var expenseViewModels = expenses.Select(expense => new ExpenseViewModel
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryName = categories.FirstOrDefault(c => c.Id == expense.CategoryId)?.Name
        }).ToList();

        ViewBag.Categories = categories;

        return View(expenseViewModels);
    }
}
