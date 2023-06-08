using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class CountryRepository
    {
        private const string EntityName = "countries";
        private readonly HttpService _httpService;

        public CountryRepository(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<CountryDto>?> GetAllAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<CountryDto>>();
                }
            }

            return null;
        }

        public async Task<CountryDto?> GetByIdAsync(long id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/{id}"));
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
