using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    //is - a
    public class Supplier 
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ShopName { get; set; }
        public string ShopLocation { get; set; }
        public string ShopLogo { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}
