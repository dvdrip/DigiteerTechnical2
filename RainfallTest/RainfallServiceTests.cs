using DigiteerTechnical2.Models;
using DigiteerTechnical2.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RainfallTest
{
    public class RainfallServiceTests
    {
        private RainfallService _rainfallService;
        private Mock<HttpClient> _mockHttpClient;
        private Mock<IOptions<AppSettings>> _mockOptions;

        [SetUp]
        public void Setup()
        {
            _mockHttpClient = new Mock<HttpClient>();
            _mockOptions = new Mock<IOptions<AppSettings>>();
            _mockOptions.Setup(x => x.Value).Returns(new AppSettings { DefaultConnection = "https://environment.data.gov.uk/" });
            _rainfallService = new RainfallService(_mockHttpClient.Object, _mockOptions.Object);
        }

        [Test]
        public async Task GetRainfallReadingsAsync_ReturnResponse_IfParametersAreValid()
        {
            // Arrange
            string id = "3680";
            int count = 3;
            string filters = "today=true";

            //var jsonResponse = @"{""readings"": [{ ""dateMeasured"": ""2024-01-11T02:30:00Z"", ""amountMeasured"": 0 }, { ""dateMeasured"": ""2024-01-11T02:30:00Z"", ""amountMeasured"": 2.63 }, { ""dateMeasured"": ""2024-01-11T00:00:00Z"", ""amountMeasured"": 0 }]}";
            //var expectedResponse = JsonConvert.DeserializeObject<RainfallReadingResponse>(jsonResponse);

            // Act
            var result = await _rainfallService.GetRainfallReadingsAsync(id, count, filters);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.readings, Has.Count.EqualTo(count));
        }

        [Test]
        public async Task GetRainfallReadingsAsync_ErrorResponse_ReturnsNull()
        {
            // Arrange
            string id = "3680";
            int count = 3;
            string filters = "today=todaywrongformat";

            var errorResponse = @"{""error"": {""code"": ""InternalError"", ""message"": ""An internal server error occurred.""}}";

            var expectedErrorHttpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(errorResponse, System.Text.Encoding.UTF8, "application/json")
            };

            //Act
            var result = await _rainfallService.GetRainfallReadingsAsync(id, count, filters);

            //Assert
            Assert.IsNull(result);
        }

    }
}