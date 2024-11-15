using Microsoft.AspNetCore.Mvc;
using Tick.Shared.Consts;
using Tick.Shared.DTO.Identity;
using Tick.Shared.Interfaces.Identity;

namespace Tick.API.Controllers.Identity
{
    [ApiController]
    [Route(Paths.BaseAccountPath)]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(Paths.RegisterPath)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            string userId = await this._identityService.RegisterUserAsync(registerDto);
            return Ok(new { UserId = userId });
        }

        [HttpPost(Paths.LoginPath)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            bool success = await this._identityService.LoginAsync(loginDto);
            if (!success)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok("Login successful.");
        }

        [HttpPost(Paths.LogoutPath)]
        public async Task<IActionResult> Logout()
        {
            await this._identityService.LogoutAsync();
            return Ok("Logged out successfully.");
        }

        [HttpGet("whoami")]
        public async Task<IActionResult> WhoAmI()
        {
            string result = await this._identityService.WhoAmI();
            return Ok(result);
        }
    }
}
