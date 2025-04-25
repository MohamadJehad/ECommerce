using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Product
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }

        public int Description { get; set; }
        public ICollection<Product> Products { get; set;} = new HashSet<Product>();
    }
}
