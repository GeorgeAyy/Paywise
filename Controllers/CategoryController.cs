using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Threading.Tasks;

namespace MyMvcApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Find(_ => true).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.InsertOneAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
