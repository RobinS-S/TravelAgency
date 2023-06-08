using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class LocationRepository
    {
        private const string EntityName = "locations";
        private readonly HttpService _httpService;

        public LocationRepository(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<LocationDto>?> GetAllAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LocationDto>>();
                }
            }

            return null;
        }

        public async Task<List<LocationDto>?> GetAllByCountryIdAsync(long countryId)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/byCountry?countryId={countryId}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<LocationDto>>();
                }
            }

            return null;
        }

        public async Task<LocationDto?> GetByIdAsync(long id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/{id}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LocationDto>();
                }
            }

            return null;
        }
    }
}
