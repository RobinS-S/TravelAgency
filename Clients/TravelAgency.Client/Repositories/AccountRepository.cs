using System.Net.Http.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Repositories
{
    public class AccountRepository
    {
        private const string EntityName = "account";
        private readonly HttpService _httpService;

        public AccountRepository(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ProfileDto?> GetByIdAsync(string id)
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/profile/{id}"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProfileDto>();
                }
            }

            return null;
        }

        public async Task<ProfileDto?> GetMineAsync()
        {
            var response = await _httpService.GetResponseAsync(new Uri($"{ApiConfig.ApiUrl}/api/{EntityName}/profile"));
            if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ProfileDto>();
                }
            }

            return null;
        }
    }
}
