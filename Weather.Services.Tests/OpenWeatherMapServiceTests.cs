using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Weather.Services.Tests
{
    [TestClass]
    public class OpenWeatherMapServiceTests
    {
        private readonly Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>();
        private readonly Mock<ILogger<IOpenWeatherMapService>> mockLogger = new Mock<ILogger<IOpenWeatherMapService>>();
        private readonly Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();

        private OpenWeatherMapService _service;

        [TestInitialize]
        public void Setup()
        {
            var httpMessageResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""Name"": ""Bothell""}")
            };

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpMessageResponse);

           

            mockConfig.SetupGet(x => x[It.Is<string>(s => s == "values:OpenWeatherMapAppId")]).Returns("FakeAppId");
            _service = new OpenWeatherMapService(new HttpClient(mockHandler.Object), mockLogger.Object, mockConfig.Object);
        }

        [TestMethod]
        public async Task TestGetWeatherData_Success()
        {
            var result = await _service.GetWeatherData("98012");
            result.Name.Should().Be("Bothell");
        }

        [TestMethod]
        public async Task TestGetWeatherData_HttpClientFailure_Should_Return_Null()
        {
            var httpMessageResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpMessageResponse);

            var service = new OpenWeatherMapService(new HttpClient(mockHandler.Object), mockLogger.Object, mockConfig.Object);
            
            var result = await service.GetWeatherData("98012");
            
            result.Should().Be(null);
        }
    }
}
