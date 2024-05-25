using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}
