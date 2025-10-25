using ECommerce.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Authorize( Roles ="Admin")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> RoleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewBag.Success = 0;
            var list = RoleManager.Roles.Select(r=>new RoleDTO
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                ViewBag.Success = 1;
                return RedirectToAction("Index");
            }
            var res = await RoleManager.CreateAsync(new IdentityRole { Name = Name });
            if (res.Succeeded)
            {
                ViewBag.Success = 2;
            }
            else {
                ViewBag.Success = 3;
            }

            return RedirectToAction("Index");
        }
    }
}
