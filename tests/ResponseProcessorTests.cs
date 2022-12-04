using Xunit;

namespace PilotAppLib.Clients.MetNorway.Tests
{
    public class ResponseProcessorTests
    {
        private readonly ResponseProcessor _responseProcessor;


        public ResponseProcessorTests()
        {
            _responseProcessor = new ResponseProcessor();
        }


        [Theory]
        [InlineData(
            "ESSA",
            "AMD ESSA 040657Z 0406/0506 36003KT 9999 BKN025 PROB40 0406/0412 BKN005 PROB40 0418/0506 BKN004=",
            "ESSA AMD 040657Z 0406/0506 36003KT 9999 BKN025 PROB40 0406/0412 BKN005 PROB40 0418/0506 BKN004="
            )]
        [InlineData(
            "ESGR",
            "ESGR AUTO 031350Z 07008KT 9999 OVC023/// M01/M03 Q1030=",
            "ESGR 031350Z AUTO 07008KT 9999 OVC023/// M01/M03 Q1030="
            )]
        [InlineData(
            "ESGR",
            "ESGR 031350Z 07008KT 9999 OVC023/// M01/M03 Q1030=",
            "ESGR 031350Z 07008KT 9999 OVC023/// M01/M03 Q1030="
            )]
        [InlineData(
            "ESGJ",
            " METAR 1 \r\n METAR 2 \r\n\r\n AUTO ESGJ 031350Z 07008KT 9999 OVC023/// M01/M03 Q1030= \n ",
            "ESGJ 031350Z AUTO 07008KT 9999 OVC023/// M01/M03 Q1030="
            )]
        public void Process(string icaoCode, string input, string expected)
        {
            string output = _responseProcessor.Process(input, icaoCode);
            Assert.Equal(expected, output);
        }
    }
}
