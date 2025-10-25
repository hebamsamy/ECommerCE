using ECommerce.Helpers;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //Work as API
            //builder.Services.AddControllers();




            #region IOC / DI Container
            //life time
            //Configuration Object 
            //builder.Services.AddSingleton
            //DbContext Object
            //Per Request
            //builder.Services.AddScoped
            //Per use/ call
            //builder.Services.AddTransient
            builder.Services.AddDbContext<EcommerceContext>(
                i => i.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddIdentity<User, IdentityRole>(i =>
            {
                i.Password.RequireNonAlphanumeric = false;
                i.Password.RequireUppercase = false;
                i.Password.RequireLowercase = false;
                i.Password.RequiredLength = 8;
                i.User.RequireUniqueEmail = true;
                i.Lockout.MaxFailedAccessAttempts = 3;
                i.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                i.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<EcommerceContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication().AddGoogle(
                option =>
                {
                    option.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"];
                    option.ClientId = builder.Configuration["GoogleAuth:ClientID"];
                    option.CallbackPath = "/Account/SignInGoogle";
                }
            );

            builder.Services.AddAuthorization(
                options =>
                {

                    options.AddPolicy("CanModifyProduct", policy =>
                    {
                        policy.RequireRole("Admin,Supplier").RequireClaim("ModifiesProduct");
                    });
                    options.AddPolicy("CanAddProduct", policy =>
                    {
                        policy.RequireRole("Supplier").RequireClaim("AddsProduct");
                    });

                }
                );

            builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsFactory>();
            //builder.Services.AddScoped<Iuuuuu, hhhhh>();
            builder.Services.AddScoped(typeof(MailService));
            builder.Services.AddScoped(typeof(ProductRepository));
            builder.Services.AddScoped(typeof(CategoryRepesitory)); 
            builder.Services.AddScoped(typeof(RoleRepository));
            builder.Services.AddScoped(typeof(SupplierRepository));
            #endregion





            var app = builder.Build();

            
            //make routes table
            app.UseRouting();



            app.MapStaticAssets();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}")
                .WithStaticAssets();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //      name: "areas",
            //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});

            app.Run();

        }
    }
}