using Moq;
using Newtonsoft.Json;
using System.Net;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Pages.Countries;
using TravelAgency.Client.Repositories;
using TravelAgency.Client.Tests.Misc;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Tests
{
    public class CountriesViewModelTest
    {
        [Fact]
        public async Task ViewModelWorks()
        {
            // Arrange
            var expectedData = new List<CountryDto>
            {
                new() { Id = 1, IsoName = "ams", Coordinates = new GeoCoordinatesDto(52.3169109, 4.745882), Images = new List<EntityImageDto>()
                {
                    new() { Id = 1, ImageId = 2, IsPrimary = true}
                }}
            };
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get,
                    HttpStatusCode.OK,
                    JsonConvert.SerializeObject(expectedData))
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);
            var repository = new CountryRepository(httpService);
            var viewModel = new CountriesPageViewModel(repository);

            // Act
            await viewModel.LoadDataCommand.ExecuteAsync(null);

            // Assert
            Assert.False(viewModel.ErrorStateEnabled);
            Assert.NotEmpty(viewModel.CountriesList);
            Assert.Equal(viewModel.CountriesList[0].Id, expectedData[0].Id);
        }

        [Fact]
        public async Task ViewModelHttpErrorHandling()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get,
                    HttpStatusCode.InternalServerError,
                    "")
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);
            var repository = new CountryRepository(httpService);
            var viewModel = new CountriesPageViewModel(repository);

            // Act
            await viewModel.LoadDataCommand.ExecuteAsync(null);

            // Assert
            Assert.Empty(viewModel.CountriesList);
            Assert.True(viewModel.ErrorStateEnabled);
        }
    }
}
