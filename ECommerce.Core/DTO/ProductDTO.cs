using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTO
{
    public record ProductDTO
    (string name, string description);
    public record UpdateProductDTO
    (string name, string description, int id);
}
