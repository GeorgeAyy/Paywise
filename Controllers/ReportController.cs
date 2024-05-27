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
        private readonly IAppLogger _logger;

        public ReportController(IReportService reportService, IAppLogger logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        public IActionResult Generate()
        {
            _logger.LogInfo("Generate report page requested.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(DateTime startDate, DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInfo($"Generate report requested by user ID: {userId}, Start Date: {startDate}, End Date: {endDate}");
            var expenses = await _reportService.GenerateReportAsync(userId, startDate, endDate);
            var reportViewModel = new Report
            {
                StartDate = startDate,
                EndDate = endDate,
                Expenses = expenses
            };
            _logger.LogInfo($"Report generated for user ID: {userId}, Start Date: {startDate}, End Date: {endDate}");
            return View("Report", reportViewModel);
        }
    }
}
