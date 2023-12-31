﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidencesController : ControllerBase
    {
        private readonly IResidenceRepository _repository;
        private readonly IMapper _mapper;

        public ResidencesController(IResidenceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ResidenceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ResidenceDto>>> GetAll()
        {
            var residences = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResidenceDto>>(residences));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResidenceDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResidenceDto>> GetById(long id)
        {
            var residence = await _repository.GetByIdAsync(id);
            if (residence == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ResidenceDto>(residence));
        }


        [HttpGet("byLocation")]
        [ProducesResponseType(typeof(IEnumerable<ResidenceDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResidenceDto>> GetAllByLocationId([FromQuery] long locationId)
        {
            var residences = await _repository.GetAllByLocationIdAsync(locationId);
            return Ok(_mapper.Map<IEnumerable<ResidenceDto>>(residences));
        }
    }

}
