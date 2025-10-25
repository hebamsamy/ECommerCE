using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class CartItem :BaseModel
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //[NotMapped]
        //public DateTime MyProperty { get; set; }


        // Navigation properties
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
