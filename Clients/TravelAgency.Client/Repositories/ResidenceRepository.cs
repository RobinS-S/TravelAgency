using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class ResidenceRepository
    {
        private const string EntityName = "residences";
        private readonly HttpService _httpService;

        public ResidenceRepository(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<ResidenceDto>?> GetAllAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ResidenceDto>>();
                }
            }

            return null;
        }

        public async Task<IEnumerable<ResidenceDto>?> GetAllByLocationIdAsync(long locationId)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/byLocation?locationId={locationId}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ResidenceDto>>();
                }
            }

            return null;
        }

        public async Task<ResidenceDto?> GetByIdAsync(long id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/{id}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ResidenceDto>();
                }
            }

            return null;
        }
    }
}
