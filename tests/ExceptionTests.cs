using Xunit;

namespace PilotAppLib.Clients.MetNorway.Tests
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
