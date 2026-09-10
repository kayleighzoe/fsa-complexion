using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductRecommendationController : ControllerBase
    {
        private readonly IProductRecommendationService _service;
        private readonly IProductRecommendationRepository _repository;

        public ProductRecommendationController(IProductRecommendationService service, IProductRecommendationRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendations()
        {
            var recommendations = await _repository.GetAllProductReccommendationsAsync();

            return Ok(recommendations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecommendation(Guid id)
        {
            var recommendation = await _service.GetProductReccommendationAsync(id);

            return Ok(recommendation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecommendation([FromBody] CreateProductRecommendationDto dto)
        {
            await _service.CreateProductReccommendationAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRecommendation(Guid id, [FromBody] UpdateProductRecommendationDto dto)
        {
            await _service.UpdateProductReccommendationAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation(Guid id)
        {
            await _repository.DeleteProductReccommendationAsync(id);

            return NoContent();
        }
    }
}