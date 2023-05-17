using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class CountryRepository
    {
        private readonly HttpService _httpService;

        public CountryRepository(HttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<IEnumerable<CountryDto>?> GetAllAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri(ApiConfig.ApiUrl + "/api/Countries"));
            if (response != null)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<CountryDto>>();
                }
            }

            return null;
        }

        public async Task<CountryDto?> GetByIdAsync(long id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/Countries/{id}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CountryDto>();
                }
            }

            return null;
        }
    }
}
