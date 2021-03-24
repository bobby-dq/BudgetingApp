using Microsoft.AspNetCore.Mvc;

namespace BudgetingApp.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}