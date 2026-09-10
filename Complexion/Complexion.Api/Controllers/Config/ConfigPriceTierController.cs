using Complexion.Repository.Config;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Config
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ConfigPriceTierController : ControllerBase
    {
        private readonly IConfigPriceTierRepository _configPriceTierRepository;

        public ConfigPriceTierController(IConfigPriceTierRepository configPriceTierRepository)
        {
            _configPriceTierRepository = configPriceTierRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPriceTiers()
        {
            var priceTiers = await _configPriceTierRepository.GetAllPriceTiersAsync();

            return Ok(priceTiers);
        }
    }
}