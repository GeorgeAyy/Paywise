using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Services;
using MyMvcApp.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyMvcApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Generate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expenses = await _reportService.GenerateReportAsync(userId, startDate, endDate);
            var reportViewModel = new Report
            {
                StartDate = startDate,
                EndDate = endDate,
                Expenses = expenses
            };
            return View("Report", reportViewModel);
        }
    }
}
