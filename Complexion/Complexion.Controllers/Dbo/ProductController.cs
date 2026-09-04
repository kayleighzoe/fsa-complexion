using Complexion.DTOs.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Dbo
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IProductRepository _repository;

        public ProductController(IProductService service, IProductRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? search)
        {
            var products = await _repository.GetAllProductsAsync(search);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _service.GetProductAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            await _service.CreateProductAsync(dto);

            return StatusCode(StatusCodes.Status201Created);

        }
    }
}