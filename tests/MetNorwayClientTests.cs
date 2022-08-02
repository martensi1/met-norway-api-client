using PilotAppLib.Clients.MetNorway;
using Moq;
using System;
using Xunit;

namespace PilotAppLib.AirportFinder.Tests
{
    public class MetNorwayClientTests
    {
        private readonly Mock<IApiClient> _apiMock;
        private readonly MetNorwayClient _client;


        public MetNorwayClientTests()
        {
            _apiMock = new Mock<IApiClient>();
            _client = new MetNorwayClient(_apiMock.Object);
        }


        [Fact]
        public void DefaultConstructor()
        {
            new MetNorwayClient();
        }

        [Fact]
        public void Dispose()
        {
            // Arrange
            _apiMock.Setup(p => p.Dispose());

            // Act
            _client.Dispose();

            // Assert
            _apiMock.Verify(p => p.Dispose(), Times.Once);
        }

        [Theory]
        [InlineData("ESGJ", "ESGJ 242250Z 21006KT 9999 BKN009 10/10 Q1005=")]
        [InlineData("ESGJ", "ESGJ 251250Z 26008KT 9999 FEW027 SCT035CB BKN150 13/11 Q1008 RERA=")]
        [InlineData("ESSA", "ESSA 242320Z 14008KT CAVOK 12/06 Q1006 NOSIG=")]
        [InlineData("ESSA", "ESSA 251050Z 19010KT 160V220 9999 SCT038 18/07 Q1006 BECMG BKN015=")]
        public void FetchMetar(string airportIcao, string metarData)
        {
            // Arrange
            _apiMock.Setup(p => p.GetReport(
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Returns(metarData);

            // Act
            string result = _client.FetchMetar(airportIcao);

            // Assert
            Assert.Equal(metarData, result);
            _apiMock.Verify(p => p.GetReport(airportIcao, "METAR"), Times.Once);
        }

        [Theory]
        [InlineData("ESGJ", "ESGJ 250830Z 2509/2515 21012KT 9999 BKN020 PROB40 2509/2515 SHRA SCT020CB=")]
        [InlineData("ESGJ", "ESGJ 251730Z 2518/2601 23012KT 9999 SCT040=")]
        [InlineData("ESSA", "ESSA 250530Z 2506/2606 21009KT 9999 BKN005 BECMG 2506/2508 SCT012 BKN015=")]
        [InlineData("ESSA", "ESSA 251130Z 2512/2612 22010KT 9999 SCT030 PROB40 2512/2521 SHRA BKN030CB PROB402609/2612 SHRA BKN030CB=")]
        public void FetchTaf(string airportIcao, string tafData)
        {
            // Arrange
            _apiMock.Setup(p => p.GetReport(
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Returns(tafData);

            // Act
            string result = _client.FetchTaf(airportIcao);

            // Assert
            Assert.Equal(tafData, result);
            _apiMock.Verify(p => p.GetReport(airportIcao, "TAF"), Times.Once);
        }

        [Fact]
        public void FetchMetarInvalidIcao()
        {
            var ex = Assert.Throws<ArgumentException>(() => _client.FetchMetar("ESS"));
            Assert.Equal("airport", ex.ParamName);
        }

        [Fact]
        public void FetchTafInvalidIcao()
        {
            var ex = Assert.Throws<ArgumentException>(() => _client.FetchTaf("ESGJJ"));
            Assert.Equal("airport", ex.ParamName);
        }

        [Fact]
        public void FetchMetarArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => _client.FetchMetar(null));
        }

        [Fact]
        public void FetchTafArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => _client.FetchTaf(null));
        }
    }
}
