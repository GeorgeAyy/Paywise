using Microsoft.AspNetCore.Mvc;

public class ExpenseController : Controller
{

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


}

