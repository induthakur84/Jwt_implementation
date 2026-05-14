using Jwt_implementation.Dto;
using Jwt_implementation.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jwt_implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //here we can implment dependency injection for product service
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("create")]
        [Authorize("Admin")]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var result = await _productService.Create(productCreateDto);
            if (result == null)
            {
                return BadRequest("Product creation failed");
            }
            return Ok(result);
        }
        [HttpGet("getall")]
        [Authorize("User, Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }
        [HttpGet("getbyid/{id}")]
        [Authorize("User, Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetById(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }
        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var result = await _productService.Update(productUpdateDto);
            if (result == null)
            {
                return BadRequest("Product update failed");
            }
            return Ok(result);
        }
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (result == "Product not found")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
