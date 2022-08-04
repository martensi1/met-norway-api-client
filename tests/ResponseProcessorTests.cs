using Xunit;

namespace PilotAppLib.Clients.MetNorway.Tests
{
    public class ResponseProcessorTests
    {
        [Theory]
        [InlineData("METAR", "METAR")]
        [InlineData(" METAR 1 \r\n METAR 2 \r\n\r\n METAR 4 ", "METAR 4")]
        public void Process(string input, string expected)
        {
            var processor = new ResponseProcessor();

            string output = processor.Process(input);
            Assert.Equal(expected, output);
        }
    }
}
