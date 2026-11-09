using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Complexion.Api.Extensions;

namespace Complexion.Api.Controllers.Products
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductRecommendationController : ControllerBase
    {
        private readonly IProductRecommendationService _productRecommendationService;
        private readonly IProductRecommendationRepository _productRecommendationRepository;
        private readonly IShareRecommendationService _shareRecommendationService;

        public ProductRecommendationController(IProductRecommendationService service, IProductRecommendationRepository repository, IShareRecommendationService shareRecommendationService)
        {
            _productRecommendationService = service;
            _productRecommendationRepository = repository;
            _shareRecommendationService = shareRecommendationService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRecommendations()
        {
            var recommendations = await _productRecommendationRepository.GetAllProductRecommendationsAsync();

            return Ok(recommendations);
        }

        [AllowAnonymous]
        [HttpGet("{recommendationId}")]
        public async Task<IActionResult> GetRecommendation(Guid recommendationId)
        {
            var recommendation = await _productRecommendationService.GetProductRecommendationAsync(recommendationId);

            return Ok(recommendation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecommendation([FromBody] ShareRecommendationDto shareRecommendationDto)
        {
            await _shareRecommendationService.ShareRecommendationAsync(User.GetUserId(), shareRecommendationDto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch("{recommendationId}")]
        public async Task<IActionResult> UpdateRecommendation(Guid recommendationId, [FromBody] UpdateProductRecommendationDto updateProductRecommentationDto)
        {
            await _productRecommendationService.UpdateProductRecommendationAsync(User.GetUserId(), recommendationId, updateProductRecommentationDto);

            return Ok();
        }

        [HttpDelete("{recommendationId}")]
        public async Task<IActionResult> DeleteRecommendation(Guid recommendationId)
        {
            await _productRecommendationService.DeleteProductRecommendationAsync(User.GetUserId(), recommendationId);

            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendationsByUserId(Guid userId)
        {
            var recommendations = await _productRecommendationService.GetProductRecommendationsByUserIdAsync(userId);

            return Ok(recommendations);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRecommendationSummaries()
        {
            var summaries = await _productRecommendationService.GetProductRecommendationSummariesAsync();

            return Ok(summaries);
        }
    }
}