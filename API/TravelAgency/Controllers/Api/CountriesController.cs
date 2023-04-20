using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly CountryRepository _repository;
        private readonly IMapper _mapper;

        public CountriesController(CountryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            var countries = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CountryDto>>(countries));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetById(long id)
        {
            var country = await _repository.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CountryDto>(country));
        }
    }
}
