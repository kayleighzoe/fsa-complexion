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
        public async Task<IActionResult> GetRecommendation(Guid recommendationId)
        {
            var recommendation = await _productRecommendationService.GetProductReccommendationAsync(recommendationId);

            return Ok(recommendation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecommendation([FromBody] CreateProductRecommendationDto createProductRecommentationDto)
        {
            await _productRecommendationService.CreateProductReccommendationAsync(createProductRecommentationDto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRecommendation(Guid recommendationId, [FromBody] UpdateProductRecommendationDto createProductRecommentationDto)
        {
            await _productRecommendationService.UpdateProductReccommendationAsync(recommendationId, createProductRecommentationDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecommendation(Guid recommendationId)
        {
            await _productRecommendationRepository.DeleteProductReccommendationAsync(recommendationId);

            return Ok();
        }
    }
}