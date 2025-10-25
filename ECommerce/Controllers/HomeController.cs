using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductRepository ProductRepository;

        public HomeController(ProductRepository _repo)
        {
            ProductRepository = _repo;
        }

        public IActionResult Index()
        {
            var products = ProductRepository.GetAll().Select(p => p.ToDTO()).ToList();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
