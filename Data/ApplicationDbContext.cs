using MongoDB.Driver;
using MyMvcApp.Models;

namespace MyMvcApp.Data
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IMongoClient client, string databaseName)
        {
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Expense> Expenses => _database.GetCollection<Expense>("Expenses");
        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public async Task<List<Category>> GetCategoriesForUserAsync(string userId)
        {
            return await Categories.Find(c => c.UserId == userId).ToListAsync();
        }

        public async Task<List<Expense>> GetExpensesForUserAsync(string userId)
        {
            return await Expenses.Find(e => e.UserId == userId).ToListAsync();
        }
    }
}
