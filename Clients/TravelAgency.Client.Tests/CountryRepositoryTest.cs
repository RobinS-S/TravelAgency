using Moq;
using System.Net;
using Newtonsoft.Json;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Repositories;
using TravelAgency.Client.Tests.Misc;
using TravelAgency.Shared.Dto;

namespace TravelAgency.Client.Tests
{
    public class CountryRepositoryTest
    {
        [Fact]
        public async Task GetAllCallsApiAndReturnsRightValue()
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

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedData[0].Id, result[0].Id);
        }
    }
}
