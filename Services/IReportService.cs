using MyMvcApp.Models;

namespace MyMvcApp.Services
{
    public interface IReportService
    {
        Task<List<Expense>> GenerateReportAsync(string userId, DateTime startDate, DateTime endDate);
    }
}
