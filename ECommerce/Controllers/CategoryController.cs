using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var  x = new ContentResult();
            x.Content = "<h1>Hello from Content Result</h1>";
            return x;
        }
    }
}
