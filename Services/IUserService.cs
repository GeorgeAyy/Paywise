using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IUserService
    {
        Task<User> ValidateUserCredentials(string username, string password);
        Task<User> FindByUsernameAsync(string username);
        Task RegisterUserAsync(User user, string password);
        Task<User> GetUserByIdAsync(string userId);
        Task<List<Expense>> GetUserExpensesAsync(string userId);
    }
}