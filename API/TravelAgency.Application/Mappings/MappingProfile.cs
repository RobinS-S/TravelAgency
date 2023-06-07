using AutoMapper;
using TravelAgency.Domain;
using TravelAgency.Domain.Entities;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Application.Mappings
{
    public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ProfileDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<EntityImage, EntityImageDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Residence, ResidenceDto>();
            CreateMap<TranslatedText, TranslatedTextDto>();
            CreateMap<GeoCoordinates, GeoCoordinatesDto>();
            CreateMap<ReservationCreateResult, ReservationCreateResultDto>();

            CreateMap<Flight, FlightDto>()
                .ForMember(f => f.Seats, b => b.MapFrom(s => s.Seats.Select(s => s.SeatNumber)));

            CreateMap<Reservation, ReservationDto>();
        }
	}
}
