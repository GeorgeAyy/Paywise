using MongoDB.Driver;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMvcApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<Expense> _expenses;

        public CategoryService(IMongoClient client)
        {
            var database = client.GetDatabase("Paywise");
            _categories = database.GetCollection<Category>("Categories");
            _expenses = database.GetCollection<Expense>("Expenses");
        }

        public async Task<List<Category>> GetCategoriesForUserAsync(string userId)
        {
            return await _categories.Find(c => c.UserId == userId).ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(string id, string userId)
        {
            var categoryFilter = Builders<Category>.Filter.Where(c => c.Id == id && c.UserId == userId);
            var categoryResult = await _categories.DeleteOneAsync(categoryFilter);
            if (categoryResult.DeletedCount > 0)
            {
                var expenseFilter = Builders<Expense>.Filter.Eq(e => e.CategoryId, id);
                await _expenses.DeleteManyAsync(expenseFilter);
                return true;
            }
            return false;
        }

        public async Task<bool> EditCategoryAsync(Category category, string userId)
        {
            var filter = Builders<Category>.Filter.Where(c => c.Id == category.Id && c.UserId == userId);
            var update = Builders<Category>.Update.Set(c => c.Name, category.Name);
            var result = await _categories.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
