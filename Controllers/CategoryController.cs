using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var categories = await _context.GetCategoriesForUserAsync(userId);
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        category.UserId = userId;
        await _context.Categories.InsertOneAsync(category);
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Debugging information
        Console.WriteLine($"Attempting to delete category with ID: {id} for user: {userId}");

        var categoryFilter = Builders<Category>.Filter.Where(c => c.Id == id && c.UserId == userId);
        var categoryResult = await _context.Categories.DeleteOneAsync(categoryFilter);

        if (categoryResult.DeletedCount > 0)
        {
            // Delete associated expenses
            var expenseFilter = Builders<Expense>.Filter.Eq(e => e.CategoryId, id);
            var expenseResult = await _context.Expenses.DeleteManyAsync(expenseFilter);

            Console.WriteLine($"Successfully deleted category with ID: {id} and {expenseResult.DeletedCount} associated expenses.");
            TempData["Message"] = "Category and associated expenses successfully deleted.";
        }
        else
        {
            Console.WriteLine($"Failed to delete category with ID: {id}");
            TempData["Error"] = "Failed to delete category.";
        }

        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var filter = Builders<Category>.Filter.Where(c => c.Id == category.Id && c.UserId == userId);
        var update = Builders<Category>.Update.Set(c => c.Name, category.Name);
        var result = await _context.Categories.UpdateOneAsync(filter, update);

        if (result.ModifiedCount > 0)
        {
            TempData["Message"] = "Category successfully updated.";
        }
        else
        {
            TempData["Error"] = "Failed to update category.";
        }

        return RedirectToAction("Manage");
    }
}
