using ECommerce.DTOs;
using ECommerce.Models;
using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repositories
{
    public class ProductRepository :BaseRepository<Product>
    {
        public ProductRepository(EcommerceContext _context) : base(_context)
        {
        }

        public PaginationDTO<ProductDetailsDTO> Search(
            string SearchText = "", decimal Price = 0, int CategoryId = 0,
            string ShopName = "", int PageSize = 3, int PageNumber = 1
            )
        {
            var filteretion = PredicateBuilder.New<Product>(true);
            var oid = filteretion;
                //||
                //&&

            if (!SearchText.IsNullOrEmpty())
            {
                filteretion = filteretion.And(p => p.Name.ToLower().Contains(SearchText.ToLower()) ||
                p.Description.ToLower().Contains(SearchText.ToLower()));
            }
            if (!ShopName.IsNullOrEmpty())
            {
                filteretion = filteretion.And(p => p.Supplier.ShopName.ToLower().Contains(SearchText.ToLower()));
            }
            if (Price > 0)
            {
                filteretion = filteretion.And(p => p.Price <= Price);
            }
            if (CategoryId > 0)
            {
                filteretion = filteretion.And(p => p.CategoryId == CategoryId);
            }

            if(filteretion == oid)
            {
                // no filteration
                filteretion = null;
            }
            var data = Get(filteretion, PageSize, PageNumber);
            int totelCountAfterFilteration = 
                filteretion== null? 
                GetAll().Count(): GetAll().Where(filteretion).Count();

            return new PaginationDTO<ProductDetailsDTO>()
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                Count = totelCountAfterFilteration ,
                List = data.Select(p=>p.ToDTO()).ToList()
            };




        }

    }
}
