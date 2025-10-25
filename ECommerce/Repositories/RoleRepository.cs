using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repositories
{
    public class RoleRepository :BaseRepository<IdentityRole>
    {
        public RoleRepository(EcommerceContext context) : base(context)
        {
        }


    }
}
