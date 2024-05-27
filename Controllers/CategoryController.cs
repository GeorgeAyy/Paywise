using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;
using MyMvcApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IAppLogger _logger;

    public CategoryController(ICategoryService categoryService, IAppLogger logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _logger.LogInfo($"Manage categories requested by user ID: {userId}");
        var categories = await _categoryService.GetCategoriesForUserAsync(userId);
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        category.UserId = userId;
        await _categoryService.AddCategoryAsync(category);
        _logger.LogInfo($"Category added for user ID: {userId}");
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _categoryService.DeleteCategoryAsync(id, userId);
        _logger.LogInfo($"Delete category {id} for user ID: {userId}, Success: {success}");
        TempData[success ? "Message" : "Error"] = success ? "Category and associated expenses successfully deleted." : "Failed to delete category.";
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _categoryService.EditCategoryAsync(category, userId);
        _logger.LogInfo($"Edit category {category.Id} for user ID: {userId}, Success: {success}");
        TempData[success ? "Message" : "Error"] = success ? "Category successfully updated." : "Failed to update category.";
        return RedirectToAction("Manage");
    }
}
