using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application;
using TravelAgency.Application.Services;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationController(IReservationRepository reservationRepository, ReservationService reservationService, IMapper mapper, UserService userService)
        {
            _reservationRepository = reservationRepository;
            _reservationService = reservationService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationDto>> GetById(long id)
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if(reservation.OwnerId != user.Id)
            {
                return Forbid();
            }

            return Ok(_mapper.Map<ReservationDto>(reservation));
        }

        [HttpGet("activeAsTenant")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservationDto>> GetActiveAsTenant()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationRepository.GetActiveByTenantIdAsync(user.Id);
            if(reservation == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReservationDto>(reservation));
        }

        [HttpGet("activeAsOwner")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetActiveAsOwner()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await _reservationRepository.GetActiveByOwnerIdAsync(user.Id);
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));
        }

        [HttpGet("mine")]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationDto>> GetMine()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await _reservationRepository.GetAllByTenantIdAsync(user.Id);
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));
        }

        [HttpGet]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(typeof(IEnumerable<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationDto>> GetAll()
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await _reservationRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ReservationDto>>(reservations));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservationCreateResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReservationCreateResultDto>> Create([FromBody] CreateReservationDto dto)
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }

            var result = _reservationService.CreateReservation(dto, user);
            return Ok(_mapper.Map<ReservationCreateResultDto>(result));
        }
    }
}
