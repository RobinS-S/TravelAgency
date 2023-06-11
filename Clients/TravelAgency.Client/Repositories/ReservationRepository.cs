using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class ReservationRepository
    {
        private const string EntityName = "reservations";
        private readonly HttpService _httpService;

        public ReservationRepository(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ReservationDto?> GetByIdAsync(long id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/{id}"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReservationDto>();
            }

            return null;
        }

        public async Task<ReservationDto?> GetActiveAsTenantAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/activeAsTenant"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReservationDto>();
            }

            return null;
        }

        public async Task<List<ReservationDto>?> GetActiveAsOwnerAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/activeAsOwner"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ReservationDto>>();
            }

            return null;
        }

        public async Task<List<ReservationDto>?> GetMineAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/mine"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ReservationDto>>();
            }

            return null;
        }

        public async Task<List<ReservationDto>?> GetAllAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ReservationDto>>();
            }

            return null;
        }

        public async Task<List<ReservationPickedSpotDto>?> GetAllBetweenAsync(long residenceId, DateTime start, DateTime end)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/between?residenceId={residenceId}&from={start}&until={end}"));
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ReservationPickedSpotDto>>();
            }

            return null;
        }

        public async Task<ReservationCreateResultDto?> CreateAsync(CreateReservationDto dto)
        {
            var response = await _httpService.PostAsJsonAsync($"{ApiConfig.ApiUrl}/api/{EntityName}", dto);
            if (response == null) return null;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReservationCreateResultDto>();
            }

            return null;
        }
    }

}
