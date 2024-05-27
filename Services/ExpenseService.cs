using MongoDB.Driver;
using MyMvcApp.Models;
using MyMvcApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMvcApp.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IMongoCollection<Expense> _expenses;
        private readonly IMongoCollection<Category> _categories;

        public ExpenseService(IMongoClient client)
        {
            var database = client.GetDatabase("Paywise");
            _expenses = database.GetCollection<Expense>("Expenses");
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<List<Expense>> GetExpensesForUserAsync(string userId)
        {
            return await _expenses.Find(e => e.UserId == userId).ToListAsync();
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _expenses.InsertOneAsync(expense);
        }

        public async Task<List<ExpenseViewModel>> GetFilteredExpensesAsync(string userId, DateTime? minDate, DateTime? maxDate, decimal? minAmount, decimal? maxAmount, string categoryId)
        {
            var expenses = await GetExpensesForUserAsync(userId);
            var categories = await _categories.Find(c => c.UserId == userId).ToListAsync();

            if (minDate.HasValue)
                expenses = expenses.Where(e => e.Date >= minDate.Value).ToList();

            if (maxDate.HasValue)
                expenses = expenses.Where(e => e.Date <= maxDate.Value).ToList();

            if (minAmount.HasValue)
                expenses = expenses.Where(e => e.Amount >= minAmount.Value).ToList();

            if (maxAmount.HasValue)
                expenses = expenses.Where(e => e.Amount <= maxAmount.Value).ToList();

            if (!string.IsNullOrEmpty(categoryId))
                expenses = expenses.Where(e => e.CategoryId == categoryId).ToList();

            return expenses.Select(expense => new ExpenseViewModel
            {
                Id = expense.Id,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CategoryName = categories.FirstOrDefault(c => c.Id == expense.CategoryId)?.Name
            }).ToList();
        }
    }
}
