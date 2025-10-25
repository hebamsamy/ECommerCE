using ECommerce.Models;

namespace ECommerce.Repositories
{
    public class SupplierRepository :BaseRepository<Supplier>
    {
        public SupplierRepository(EcommerceContext _context) : base(_context)
        {
        }
    }
}
