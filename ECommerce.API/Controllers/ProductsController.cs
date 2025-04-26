using AutoMapper;
using ECommerce.API.Helper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Product;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(x => x.Category, x=> x.Photos);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var products = await work.ProductRepository.GetByIdAsync(id);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> add(ProductDTO productDTO)
        {
            try
            {
                var product = mapper.Map<Product>(productDTO);

                await work.ProductRepository.AddAsync(product);
                return Ok(new ResponseAPI(200, "Item added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> add(UpdateProductDTO productDTO)
        {
            try
            {
                var product = mapper.Map<Product>(productDTO);

                await work.ProductRepository.UpdateAsync(product);
                return Ok(new ResponseAPI(200, "Item Updated"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.ProductRepository.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
