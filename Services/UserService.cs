using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMvcApp.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Expense> _expenses;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IMongoClient client, IPasswordHasher<User> passwordHasher)
        {
            var database = client.GetDatabase("Paywise");
            _users = database.GetCollection<User>("Users");
            _expenses = database.GetCollection<Expense>("Expenses");
            _passwordHasher = passwordHasher;
        }

        public async Task<User> ValidateUserCredentials(string username, string password)
        {
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null && _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
            {
                return user;
            }
            return null;
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            await _users.InsertOneAsync(user);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _users.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<List<Expense>> GetUserExpensesAsync(string userId)
        {
            return await _expenses.Find(e => e.UserId == userId).ToListAsync();
        }
    }
}
