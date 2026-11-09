using Complexion.Api.Extensions;
using Complexion.Models.Auth;
using Complexion.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var profile = await _userService.GetProfileAsync(User.GetUserId());

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
        {
            await _userService.UpdateProfileAsync(User.GetUserId(), dto);

            return Ok();
        }
    }
}