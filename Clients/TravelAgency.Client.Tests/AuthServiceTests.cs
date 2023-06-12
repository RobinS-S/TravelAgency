using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using TravelAgency.Client.Auth.Services;
using TravelAgency.Client.Tests.Misc;

namespace TravelAgency.Client.Tests
{
    public class AuthServiceTest
    {
        [Fact]
        public async Task LogoutWorksAndResetsAuthToken()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("testAuthToken"));
            var actualToken = string.Empty;
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);

            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.OK, "profile data here", expectedUri)
                .Callback<HttpRequestMessage, CancellationToken>((req, token) =>
                {
                    if (req.Headers.Authorization is { Parameter: not null })
                    {
                        actualToken = req.Headers.Authorization.Parameter;
                    }
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            authService.LoadToken(expectedToken);
            authService.LoadToken(null);
            await httpService.TestLogin();

            // Assert
            Assert.False(authService.HasAuthToken);
            Assert.Equal(actualToken, string.Empty);
        }

        [Fact]
        public async Task LoginCheckCallsRightUrl()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.OK, "profile data here", expectedUri)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            await httpService.TestLogin();

            // Assert
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task LoginCheck()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.OK, "profile data here", expectedUri)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            var loginWorks = await httpService.TestLogin();

            // Assert
            Assert.True(loginWorks);
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task LoginCheckWhenForbidden()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.Forbidden, "forbidden", expectedUri)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            var loginWorks = await httpService.TestLogin();

            // Assert
            Assert.False(loginWorks);
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task LoginCheckIfUnauthorized()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);
            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.Unauthorized, "not authorized", expectedUri)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            var loginWorks = await httpService.TestLogin();

            // Assert
            handlerMock.Protected().Verify("SendAsync", Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());
            Assert.False(loginWorks);
        }

        [Fact]
        public async Task LoginCheckSendsRightBearerToken()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var expectedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes("testAuthToken"));
            var actualToken = string.Empty;
            var expectedUri = new Uri(ApiConfig.ApiTestAuthenticatedUrl);

            HttpMockHelpers.SetupHttpHandlerMock(handlerMock, HttpMethod.Get, HttpStatusCode.Unauthorized, "not authorized", expectedUri)
                .Callback<HttpRequestMessage, CancellationToken>((req, token) =>
                {
                    if (req.Headers.Authorization is { Parameter: not null })
                    {
                        actualToken = req.Headers.Authorization.Parameter;
                    }
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var authService = new AuthService(httpClient);
            var httpService = new HttpService(httpClient, authService);

            // Act
            authService.LoadToken(expectedToken);
            await httpService.TestLogin();

            // Assert
            Assert.Equal(expectedToken, actualToken);
        }
    }
}