using PilotAppLib.Clients.MetNorway;
using Moq;
using Xunit;

namespace PilotAppLib.AirportFinder.Tests
{
    public class ApiClientTests
    {
        private readonly Mock<IHttpGateway> _httpGatewayMock;
        private readonly Mock<EndpointBuilder> _endpointBuilderMock;
        private readonly Mock<ResponseProcessor> _responseProcessorMock;
        private readonly ApiClient _client;


        public ApiClientTests()
        {
            _httpGatewayMock = new Mock<IHttpGateway>();
            _endpointBuilderMock = new Mock<EndpointBuilder>();
            _responseProcessorMock = new Mock<ResponseProcessor>();

            _client = new ApiClient(
                _endpointBuilderMock.Object,
                _httpGatewayMock.Object,
                _responseProcessorMock.Object
                );
        }


        [Theory]
        [InlineData(
            "ESGJ",
            "METAR",
            "ESGJ 252320Z 01008KT 2000 BKN009 SCT002 10/10 Q1013=\r\nESGJ 242250Z 21006KT 9999 BKN009 10/10 Q1005=",
            "ESGJ 242250Z 21006KT 9999 BKN009 10/10 Q1005="
            )]
        [InlineData(
            "ESGJ",
            "TAF",
            "ESGJ 260830Z 2609/2615 23012G25KT 9999 -SHRA SCT012 BKN025 TEMPO 2609/2611 4000 SHRA BKN012 BKN030CB",
            "ESGJ 260830Z 2609/2615 23012G25KT 9999 -SHRA SCT012 BKN025 TEMPO 2609/2611 4000 SHRA BKN012 BKN030CB"
            )]
        public void GetReport(string airportIcao, string reportType, string responseData, string expectedMetar)
        {
            // Arrange
            _httpGatewayMock.Setup(p => p.SendRequest(
                It.IsAny<string>()
            )).Returns(responseData);

            // Act
            string result = _client.GetReport(airportIcao, reportType);

            // Assert
            Assert.Equal(expectedMetar, result);
            _httpGatewayMock.Verify(p => p.SendRequest(GetEndpoint(airportIcao, reportType)), Times.Once);
        }

        [Theory]
        [InlineData("ESGG", "METAR")]
        [InlineData("ESGG", "TAF")]
        public void NoDataAvailable(string airportIcao, string reportType)
        {
            // Arrange
            _httpGatewayMock.Setup(p => p.SendRequest(
                It.IsAny<string>()
            )).Returns("no-content");

            // Act
            var ex = Assert.Throws<NoDataAvailableException>(() =>
            {
                string result = _client.GetReport(airportIcao, reportType);
            });

            // Assert
            Assert.Equal(airportIcao, ex.Airport);
            Assert.Equal(reportType, ex.DataType);
        }

        [Fact]
        public void Dispose()
        {
            // Arrange
            _httpGatewayMock.Setup(p => p.Dispose());

            // Act
            _client.Dispose();

            // Assert
            _httpGatewayMock.Verify(p => p.Dispose(), Times.Once);
        }


        private string GetEndpoint(string airportIcao, string reportType)
        {
            return (new EndpointBuilder())
                .BuildHttpEndpoint(reportType, airportIcao);
        }
    }
}
