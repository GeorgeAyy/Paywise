using MyMvcApp.Models;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpensesForUserAsync(string userId);
        Task AddExpenseAsync(Expense expense);
        Task<List<ExpenseViewModel>> GetFilteredExpensesAsync(string userId, DateTime? minDate, DateTime? maxDate, decimal? minAmount, decimal? maxAmount, string categoryId);
        Task DeleteExpenseAsync(string id);
    }
}
