using ECommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Order : BaseModel
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
