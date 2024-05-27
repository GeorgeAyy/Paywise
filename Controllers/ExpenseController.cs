using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;
using MyMvcApp.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyMvcApp.ViewModels;

[Authorize]
public class ExpenseController : Controller
{
    private readonly IExpenseService _expenseService;
    private readonly ICategoryService _categoryService;

    public ExpenseController(IExpenseService expenseService, ICategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var expenses = await _expenseService.GetExpensesForUserAsync(userId);
        var categories = await _categoryService.GetCategoriesForUserAsync(userId);
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
        var categories = await _categoryService.GetCategoriesForUserAsync(userId);
        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Expense expense)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        expense.UserId = userId;
        await _expenseService.AddExpenseAsync(expense);
        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> ViewExpenses(int page = 1, int pageSize = 10, DateTime? minDate = null, DateTime? maxDate = null, decimal? minAmount = null, decimal? maxAmount = null, string categoryId = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var expenseViewModels = await _expenseService.GetFilteredExpensesAsync(userId, minDate, maxDate, minAmount, maxAmount, categoryId);
        var paginatedExpenses = expenseViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.Categories = await _categoryService.GetCategoriesForUserAsync(userId);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(expenseViewModels.Count / (double)pageSize);
        return View(paginatedExpenses);
    }
}
