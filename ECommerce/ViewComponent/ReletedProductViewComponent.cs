using ECommerce.DTOs;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce
{
    //mini controller

    public class ReletedProductViewComponent : ViewComponent
    {
        private readonly ProductRepository ProductRepository;
        public ReletedProductViewComponent(ProductRepository _repo)
        {
            ProductRepository = _repo;
        }
        public IViewComponentResult Invoke(int categoryId = 0, string supplierId = "")
        {
            if (categoryId > 0)
            {
                ViewBag.Title = "Related Products By Category";
                var products = ProductRepository.Get(p => p.CategoryId == categoryId).Take(3).Select(p => p.ToDTO()).ToList();
                return View(products);
                
            }
            else if (string.IsNullOrEmpty(supplierId))
            {
                ViewBag.Title = "Related Products By Supplier";
                var products = ProductRepository.Get(p => p.SupplierId == supplierId).Take(3).Select(p => p.ToDTO()).ToList();
                return View(products);
            }
            else
            {
                ViewBag.Title = "Related Products";
                var products = ProductRepository.GetAll().Take(3).Select(p => p.ToDTO()).ToList();
                return View(products);
            }
        }

    }
}
