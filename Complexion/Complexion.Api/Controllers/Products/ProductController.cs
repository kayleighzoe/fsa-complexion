using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductController(IProductService service, IProductRepository repository)
        {
            _productService = service;
            _productRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? search)
        {
            var products = await _productRepository.GetAllProductsAsync(search);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productService.GetProductAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            await _productService.CreateProductAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}