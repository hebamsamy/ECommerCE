using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
