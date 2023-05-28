using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
        {
            var locations = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetById(long id)
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocationDto>(location));
        }


        [HttpGet("byCountry")]
        public async Task<ActionResult<LocationDto>> GetAllByCountryId([FromQuery] long countryId)
        {
            var locations = await _repository.GetAllByCountryIdAsync(countryId);
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<LocationDto>>(locations));
        }
    }

}
