using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using ECommerce.Services;
namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly RoleRepository RoleRepository;
        private readonly SupplierRepository SupplierRepository;
        private readonly MailService MailService;

        public AccountController(
            UserManager<User> userManager,
            RoleRepository roleRepository,
            SignInManager<User> signInManager,
            SupplierRepository supplierRepository,
            MailService mailService)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
            SignInManager = signInManager;
            SupplierRepository = supplierRepository;
            MailService = mailService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if (ModelState.IsValid)
            {
                //search for user by user name or email
                var ExitstingUser = await UserManager.FindByNameAsync(user.Text);
                //found User By Username
                if (ExitstingUser == null)
                {
                    //found User By Email

                    ExitstingUser = await UserManager.FindByEmailAsync(user.Text);
                }
                if(ExitstingUser == null)
                {
                    ModelState.AddModelError("", "Sorry Register First or Ensure Of Account Inforamtion");
                    return View();
                }
                var SignInResult = await SignInManager.PasswordSignInAsync(ExitstingUser, user.Password, user.RememberMe, lockoutOnFailure: true);

                if (SignInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (SignInResult.IsLockedOut || SignInResult.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Your Account is Under Reviw Will Connet with you Later Stay tunned");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Name Or Password");
                    return View();
                }



               

            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = GetAllRolea();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO user)
        {
            // Registration logic here (e.g., save user to database)
            if (ModelState.IsValid)
            {
                var userModel = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName= user.FullName
                };
                var res =  await UserManager.CreateAsync(userModel,password:user.Password );
                if (res.Succeeded)
                {
                    await UserManager.AddToRoleAsync(userModel, user.Role);

                    if (user.Role == "Supplier")
                    {
                        //
                        SupplierRepository.Add(new Supplier
                        {
                            //UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            UserId = userModel.Id,
                            ShopName = $"{user.FullName}'s Shop"
                        });
                        SupplierRepository.UnitofWork();
                        await UserManager.AddClaimAsync(userModel, new Claim("AddsProduct", "true"));
                        await UserManager.AddClaimAsync(userModel, new Claim("ModifiesProduct", "true"));
                    }
                    else if(user.Role == "Admin")
                    {
                        await UserManager.AddClaimAsync(userModel, new Claim("ModifiesProduct", "true"));
                    }

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            ViewBag.Roles = GetAllRolea();
            return View();
        }


        public async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.Sucess = false;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDTO changePasswordDTO)
        {
            if(!ModelState.IsValid)
                return View();
            else
            {
                var existingUser =  await UserManager.GetUserAsync(User);
                var res= await UserManager.ChangePasswordAsync(
                    existingUser,
                    changePasswordDTO.OldPassword,
                    changePasswordDTO.NewPassword);

                if (res.Succeeded) { 
                    ViewBag.Sucess = true;
                    return View();
                }
                else
                {
                    ViewBag.Sucess = false;
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            ViewBag.SucessStatus = 0;
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> ForgetPassword(UserForgetPasswordDTO passwordDTO)
        {
            if (string.IsNullOrEmpty(passwordDTO.Email))
            {
                ViewBag.SucessStatus = 1;
                return View();
            }
            else
            {
                var ExistingUser =await UserManager.FindByEmailAsync(passwordDTO.Email);
                if (ExistingUser == null)
                {
                    ViewBag.SucessStatus = 1;
                    return View();
                }
                var token = await UserManager.GeneratePasswordResetTokenAsync(ExistingUser);
                if (!string.IsNullOrEmpty(token))
                {
                    MailService.SendMessage(passwordDTO.Email, "Reset Your Password", $"Your Verification Code : {token} ");
                    ViewBag.SucessStatus = 2;
                    return View();
                }
                ViewBag.SucessStatus = 1;
                return View();
            }
        }

        [HttpGet]
        public IActionResult VerifyForgetPasswordCode()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyForgetPasswordCode(UserVerifyForgetPasswordCodeDTO dto)
        {
            if (ModelState.IsValid) { 
                var existingUser = await UserManager.FindByEmailAsync(dto.Email);
                if (existingUser != null)
                {
                    var res = await UserManager.ResetPasswordAsync(existingUser, dto.Code, dto.NewPassword);

                    if (res.Succeeded)
                    {
                        ViewBag.SucessStatus = 2;
                        return View();
                    }
                    else {
                        ViewBag.SucessStatus = 1;
                        foreach (var item in res.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View();
                    }
                }
                else
                {
                    ViewBag.SucessStatus = 1;
                    ModelState.AddModelError("", "This Account Is NOT Exist!!!!");
                    return View();
                }
            }
            else
            {
                ViewBag.SucessStatus = 1;
                return View();
            }
        }


        private List<SelectListItem> GetAllRolea()
        {
            return RoleRepository.GetAll ().Select(r=>new SelectListItem{ Text=r.Name,Value=r.Name} ).ToList();
        }

        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            // Try sign-in by provider
            var signInResult = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if (signInResult.Succeeded)
                return LocalRedirect(returnUrl);

            // If user doesn't exist, create a new user
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);

            var user = new User { UserName = email, Email = email , FullName = name??email };

            var result = await UserManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await UserManager.AddLoginAsync(user, info);
                await SignInManager.SignInAsync(user, false);
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction(nameof(Login));
        }


    }
}
