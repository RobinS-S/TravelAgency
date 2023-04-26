using AutoMapper;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Helpers;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Application.Mappings
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryImage, CountryImageDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Residence, ResidenceDto>();
            CreateMap<TranslatedText, TranslatedTextDto>();
            CreateMap<GeoCoordinates, GeoCoordinatesDto>();
        }
	}
}
