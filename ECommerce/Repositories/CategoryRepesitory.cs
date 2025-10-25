using ECommerce.Models;

namespace ECommerce.Repositories
{
    public class CategoryRepesitory :BaseRepository<Category>
    {

        public CategoryRepesitory(EcommerceContext _context) : base(_context)
        {
        }
    }
}
