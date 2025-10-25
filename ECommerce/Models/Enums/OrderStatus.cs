using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Enums
{
    public enum OrderStatus
    {
        Panding = 0,
        Completed = 1,
        Canceled = 2,
        OnTheWay = 3,
        Delivered = 4,
        Returned = 5,
    }
}
