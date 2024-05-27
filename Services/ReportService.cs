using MongoDB.Driver;
using MyMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMvcApp.Services
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<Expense> _expenses;

        public ReportService(IMongoClient client)
        {
            var database = client.GetDatabase("Paywise");
            _expenses = database.GetCollection<Expense>("Expenses");
        }

        public async Task<List<Expense>> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await _expenses.Find(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate).ToListAsync();
        }
    }
}
