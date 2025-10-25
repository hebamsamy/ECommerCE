using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    /// <summary>
    /// ////////////////////no base model (using IDENTITY)
    /// </summary>
    public class User :IdentityUser
    {
        
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }

        public Address Address { get; set; }
        public virtual Supplier? Supplier { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public  virtual ICollection<CartItem> CartItems { get; set; }
    }
}
