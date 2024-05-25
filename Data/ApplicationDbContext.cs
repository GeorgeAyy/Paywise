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
    }
}
