using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    //MVC

    
    public class ProductController : Controller
    {
        private ProductRepository productRepository;
        private CategoryRepesitory CategoryRepesitory;
        public ProductController(ProductRepository _productRepository, CategoryRepesitory _CategoryRepesitory)
        {
            CategoryRepesitory = _CategoryRepesitory;
            productRepository = _productRepository;
        }
        [HttpGet]
        public IActionResult Index(
            string SearchText = "", decimal Price = 0, int CategoryId = 0,
            string ShopName = "", int PageSize = 3, int PageNumber = 1
            )
        {
            ViewData["Categories"] = GetCatagories();
            var data = productRepository.Search(SearchText, Price, CategoryId, ShopName,PageSize, PageNumber);
            return View(data);
            //pasing by model
        }

        public IActionResult Details(int id)
        {
            var claims = User.Claims;
            if (id > 0)
            {
                var data = productRepository.Get(p => p.Id == id).Select(p=> p.ToDTO()).FirstOrDefault();

                return View(data);

            }
            return RedirectToAction("index");
        }

        [HttpGet]
        //[Authorize (Roles ="Supplier")]
        [Authorize (Policy = "CanAddProduct")]
        public IActionResult Add()
        {
            //no casting
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");

            //casting
            ViewData["Categories"] = GetCatagories();
            //TempData["teet"] = 1;
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanAddProduct")]
        public IActionResult Add(ProductCreationDTO data)
        {
            data.SupplierId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //MAPP
            if (ModelState.IsValid) {

                data.ImageUrls = new List<string>();
                foreach (FormFile file in data.Images)
                {
                    if (file.Length > 0)
                    {
                        FileStream fileStream = new FileStream(
                            Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot","Images","Products",file.FileName
                            ), FileMode.Create);

                        file.CopyTo(fileStream);
                        fileStream.Close();

                        data.ImageUrls.Add($"/Images/Products/{file.FileName}");

                    }

                }


                Product product = new Product()
                {
                    Name = data.Name,
                    Price = data.Price,
                    Quantity = data.Quantity,
                    Description = data.Description,
                    ExpireDate = data.ExpireDate,
                    CategoryId = data.CategoryId,
                    SupplierId = data.SupplierId,
                    PublishDate = DateTime.Now,
                    ProductImages = data.ImageUrls
                    .Select(url => new ProductImage { ImageUrl = url }).ToList()
                };
                ////save to database
                productRepository.Add(product);
                productRepository.UnitofWork();
            }
            else
            {
                ViewData["Categories"] = GetCatagories();

                return View();
            }
            return RedirectToAction("Index");
        }




        [Authorize(Policy = "CanModifyProduct")]
        public IActionResult Delete(int id)
        {
            var product = productRepository.Get(p => p.Id == id).FirstOrDefault();
            productRepository.Delete(product);
            productRepository.UnitofWork();
            return RedirectToAction("Index");
        }
        private List<SelectListItem> GetCatagories()
        {
            List<SelectListItem> options = CategoryRepesitory.Get()
               .Select(c => new SelectListItem()
               {
                   Text = c.Name,
                   Value = c.Id.ToString()
               }).ToList();

            
           return options;
        }
    }
}
