using Complexion.Services.Config;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Config
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ConfigPriceTierController : ControllerBase
    {
        private readonly IConfigPriceTierService _configPriceTierService;

        public ConfigPriceTierController(IConfigPriceTierService configPriceTierService)
        {
            _configPriceTierService = configPriceTierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var priceTiers = await _configPriceTierService.GetAllAsync();
            return Ok(priceTiers);
        }
    }
}