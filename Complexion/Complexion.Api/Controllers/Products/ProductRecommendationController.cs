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

        public ProductRecommendationController(IProductRecommendationService service, IProductRecommendationRepository repository)
        {
            _productRecommendationService = service;
            _productRecommendationRepository = repository;
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
        public async Task<IActionResult> CreateRecommendation([FromBody] CreateProductRecommendationDto createProductRecommentationDto)
        {
            await _productRecommendationService.CreateProductRecommendationAsync(User.GetUserId(), createProductRecommentationDto);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch("{recommendationId}")]
        public async Task<IActionResult> UpdateRecommendation(Guid recommendationId, [FromBody] UpdateProductRecommendationDto createProductRecommentationDto)
        {
            await _productRecommendationService.UpdateProductRecommendationAsync(User.GetUserId(), recommendationId, createProductRecommentationDto);

            return Ok();
        }

        [HttpDelete("{recommendationId}")]
        public async Task<IActionResult> DeleteRecommendation(Guid recommendationId)
        {
            await _productRecommendationService.DeleteProductRecommendationAsync(User.GetUserId(), recommendationId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMyRecommendations()
        {
            var recommendations = await _productRecommendationService.GetMyProductRecommendationsAsync(User.GetUserId());

            return Ok(recommendations);
        }
    }
}