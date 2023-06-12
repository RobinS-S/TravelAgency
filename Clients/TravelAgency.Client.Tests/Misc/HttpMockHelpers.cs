using Moq.Protected;
using Moq;
using System.Net;
using Moq.Language.Flow;

namespace TravelAgency.Client.Tests.Misc
{
    public class HttpMockHelpers
    {
        public static IReturnsResult<HttpMessageHandler> SetupHttpHandlerMock(Mock<HttpMessageHandler> handlerMock, HttpMethod expectedMethod, HttpStatusCode responseStatusCode, string responseContent, Uri? expectedUri = null)
        {
            return handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == expectedMethod && (expectedUri == null || req.RequestUri == expectedUri)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = responseStatusCode,
                    Content = new StringContent(responseContent),
                });
        }
    }
}
