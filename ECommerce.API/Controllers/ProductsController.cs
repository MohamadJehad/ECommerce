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
        public async Task<IActionResult> get(string sort, int? CategoryId, int pageSize, int pageNumber)
        {
            try
            {
                var products = await work.ProductRepository.GetAllAsync(sort, CategoryId, pageSize, pageNumber);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }

                var res = mapper.Map<List<ProductDTO>>(products);
                return Ok(res);
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
                var products = await work.ProductRepository.GetByIdAsync(id, x => x.Category, x=>x.Photos);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var res = mapper.Map<ProductDTO>(products);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> add(AddProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.AddAsync(productDTO);
                return Ok(new ResponseAPI(200, "Product added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> update(UpdateProductDTO productDTO)
        {
            try
            {
                await work.ProductRepository.UpdateAsync(productDTO);
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
                var product = await work.ProductRepository.GetByIdAsync(id, x=> x.Photos, x=>x.Category);

                await work.ProductRepository.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
