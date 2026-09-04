using Complexion.DTOs.Dbo;
using Complexion.Services.Dbo;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Dbo
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? search)
        {
            var products = await _service.GetAllProductsAsync(search);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductsById(Guid id)
        {
            var product = await _service.GetProductsByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var created = await _service.CreateProductAsync(dto);

            return CreatedAtAction(nameof(GetProductsById), new {id = created.ProductId}, created);

        }
    }
}