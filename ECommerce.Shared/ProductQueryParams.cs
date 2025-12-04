using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Search { get; set; }
    }
}
