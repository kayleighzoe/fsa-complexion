using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductRecommendationController : ControllerBase
    {
        private readonly IProductRecommendationService _productRecommendationService;
        private readonly IProductRecommendationRepository _productRecommendationRepository;

        public ProductRecommendationController(IProductRecommendationService service, IProductRecommendationRepository repository)
        {
            _productRecommendationService = service;
            _productRecommendationRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecommendations()
        {
            var recommendations = await _productRecommendationRepository.GetAllProductReccommendationsAsync();

            return Ok(recommendations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecommendation(Guid id)
        {
            var recommendation = await _productRecommendationService.GetProductReccommendationAsync(id);

            return Ok(recommendation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecommendation([FromBody] CreateProductRecommendationDto dto)
        {
            await _productRecommendationService.CreateProductReccommendationAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRecommendation(Guid id, [FromBody] UpdateProductRecommendationDto dto)
        {
            await _productRecommendationService.UpdateProductReccommendationAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation(Guid id)
        {
            await _productRecommendationRepository.DeleteProductReccommendationAsync(id);

            return NoContent();
        }
    }
}