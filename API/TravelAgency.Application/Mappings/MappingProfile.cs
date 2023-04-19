using AutoMapper;
using TravelAgency.Domain.Entities;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Application.Mappings
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();
        }
	}
}
