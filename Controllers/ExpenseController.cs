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
    private readonly IAppLogger _logger;

    public ExpenseController(IExpenseService expenseService, ICategoryService categoryService, IAppLogger logger)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _logger.LogInfo($"Expenses index page requested by user ID: {userId}");
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
        _logger.LogInfo($"Add expense page requested by user ID: {userId}");
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
        _logger.LogInfo($"Expense added for user ID: {userId}");
        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> ViewExpenses(int page = 1, int pageSize = 10, DateTime? minDate = null, DateTime? maxDate = null, decimal? minAmount = null, decimal? maxAmount = null, string categoryId = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            _logger.LogError("Unauthorized access attempt to view expenses.");
            return Unauthorized();
        }

        _logger.LogInfo($"View expenses page requested by user ID: {userId}");
        var expenseViewModels = await _expenseService.GetFilteredExpensesAsync(userId, minDate, maxDate, minAmount, maxAmount, categoryId);
        var paginatedExpenses = expenseViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.Categories = await _categoryService.GetCategoriesForUserAsync(userId);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(expenseViewModels.Count / (double)pageSize);
        return View(paginatedExpenses);
    }

      [HttpPost]
    public async Task<IActionResult> DeleteExpense(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogError("DeleteExpense called with null or empty ID.");
            return BadRequest();
        }

        await _expenseService.DeleteExpenseAsync(id);
        _logger.LogInfo($"Expense with ID: {id} deleted by user ID: {userId}");

        return RedirectToAction("ViewExpenses");
    }

    
}
