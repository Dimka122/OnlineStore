using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Online_Store.Models;
using Online_Store.Models.DTOs;
using Online_Store.Services;
using System.Security.Claims;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, ITokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration=configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized();

            var tokenResponse = _tokenService.CreateToken(user);

            user.RefreshToken = tokenResponse.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays"));
            await _userManager.UpdateAsync(user);

            return Ok(tokenResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized();

            var tokenResponse = _tokenService.CreateToken(user);

            user.RefreshToken = tokenResponse.RefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(tokenResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequestDto request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = "New",
                LastName = "User"
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }
    }
}
