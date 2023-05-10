using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidencesController : ControllerBase
    {
        private readonly ResidenceRepository _repository;
        private readonly IMapper _mapper;

        public ResidencesController(ResidenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidenceDto>>> GetAll()
        {
            var residences = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResidenceDto>>(residences));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResidenceDto>> GetById(long id)
        {
            var residence = await _repository.GetByIdAsync(id);
            if (residence == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ResidenceDto>(residence));
        }
    }

}
