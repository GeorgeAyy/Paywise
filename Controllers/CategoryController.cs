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

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Manage()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var categories = await _categoryService.GetCategoriesForUserAsync(userId);
        return View(categories);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        category.UserId = userId;
        await _categoryService.AddCategoryAsync(category);
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _categoryService.DeleteCategoryAsync(id, userId);
        TempData[success ? "Message" : "Error"] = success ? "Category and associated expenses successfully deleted." : "Failed to delete category.";
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _categoryService.EditCategoryAsync(category, userId);
        TempData[success ? "Message" : "Error"] = success ? "Category successfully updated." : "Failed to update category.";
        return RedirectToAction("Manage");
    }
}
