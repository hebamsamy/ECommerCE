using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
