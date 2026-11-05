using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.IdentityModule;
using Shared.DTOs.OrderModule;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok(await serviceManager.authenticationService.LoginAsync(loginDto));
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto)
            => Ok(await serviceManager.authenticationService.RegisterAsync(registerDto));
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
            => Ok(await serviceManager.authenticationService.CheckEmailExist(email));
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync(string email)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.authenticationService.GetCurrentUserAsync(Email));
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetAddressAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.authenticationService.GetAddressAsync(Email));
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddressAsync(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.authenticationService.UpdateAddressAsync(Email, addressDto));
        }

    }
}
