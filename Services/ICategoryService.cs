using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesForUserAsync(string userId);
        Task AddCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(string id, string userId);
        Task<bool> EditCategoryAsync(Category category, string userId);
    }
}
