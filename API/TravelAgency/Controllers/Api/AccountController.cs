using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using TravelAgency.Domain.Entities;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [Authorize(AuthenticationSchemes = IdentityServerJwtConstants.IdentityServerJwtBearerScheme)]
    [ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

		[HttpGet("profile")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<ProfileDto>(user);
            return Ok(userDto);
        }
	}
}