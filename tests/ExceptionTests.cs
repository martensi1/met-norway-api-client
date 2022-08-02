using PilotAppLib.Clients.MetNorway;
using Xunit;

namespace PilotAppLib.AirportFinder.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void NoDataAvailableException()
        {
            // Act
            var ex = new NoDataAvailableException("ESSI", "TAF");

            // Assert
            Assert.Equal("ESSI", ex.Airport);
            Assert.Equal("TAF", ex.DataType);
        }
    }
}
