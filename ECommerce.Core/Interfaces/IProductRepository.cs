using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync(string sort);
        Task<bool> AddAsync(AddProductDTO productDTO);
        Task<bool> UpdateAsync(UpdateProductDTO productDTO);
        Task<bool> DeleteAsync(Product product);
    }
}
