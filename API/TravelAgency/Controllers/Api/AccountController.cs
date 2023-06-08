using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public AccountController(UserService userManager, IMapper mapper)
        {
            _userService = userManager;
            _mapper = mapper;
        }

        [HttpGet("profile")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentProfile()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<ProfileDto>(user);
            return Ok(userDto);
        }

        [HttpGet("profile/{id}")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCurrentProfile(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<ProfileDto>(user);
            return Ok(userDto);
        }
    }
}