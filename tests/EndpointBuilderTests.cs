using System;
using Xunit;

namespace PilotAppLib.Clients.MetNorway.Http.Tests
{
    public class EndpointBuilderTests
    {
        private const string ApiBaseUrl = "https://api.met.no/weatherapi/tafmetar/1.0";
        private readonly EndpointBuilder _builder;


        public EndpointBuilderTests()
        {
            _builder = new EndpointBuilder();
        }


        [Theory]
        [InlineData("METAR", "ESGJ", "/metar?icao=ESGJ&extended=true")]
        [InlineData("METAR", "ESSA", "/metar?icao=ESSA&extended=true")]
        [InlineData("TAF", "ESGJ", "/taf?icao=ESGJ&extended=true")]
        [InlineData("TAF", "ESSA", "/taf?icao=ESSA&extended=true")]
        public void BuildEndpoint(string dataType, string airportIcao, string expectedRelativeUrl)
        {
            string output = _builder.BuildHttpEndpoint(dataType, airportIcao);
            Assert.Equal($"{ApiBaseUrl}{expectedRelativeUrl}", output);
        }

        [Fact]
        public void InvalidDataType()
        {
            Assert.Throws<ArgumentException>(() =>  _builder.BuildHttpEndpoint("NOTAM", "ESSA"));
        }

        [Fact]
        public void NullDataType()
        {
            Assert.Throws<ArgumentNullException>(() => _builder.BuildHttpEndpoint(null, "ESSA"));
        }
    }
}
