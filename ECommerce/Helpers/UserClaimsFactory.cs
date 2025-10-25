using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ECommerce.Helpers
{
    public class UserClaimsFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public UserClaimsFactory (
            UserManager<User> userManager,
            IOptions<IdentityOptions>  options,
            RoleManager<IdentityRole> roleManager
            ):base(userManager,roleManager, options)
        {

        }


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claims = await base.GenerateClaimsAsync(user);
            claims.AddClaim(new Claim("FullName", user.FullName));
            //claims.AddClaim(new Claim("PhoneNumer", user.PhoneNumber));

            return claims;
        }
    }
}
