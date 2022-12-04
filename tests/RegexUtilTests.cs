using Xunit;

namespace PilotAppLib.Clients.MetNorway.Support.Tests
{
    public class RegexUtilTests
    {
        [Theory]
        [InlineData(
            "ESGJ 040750Z 05009KT 9000 BKN009/// OVC011/// M01/M02 Q1030=",
            @"(?<=\d{6}Z) ",
            " AUTO ",
            "ESGJ 040750Z AUTO 05009KT 9000 BKN009/// OVC011/// M01/M02 Q1030=",
            true
            )]
        [InlineData(
            "112",
            "1",
            "",
            "12",
            true
            )]
        [InlineData(
            "112",
            "3",
            "",
            "112",
            false
            )]
        public void ReplaceFirstOccurence(string input, string pattern,
            string replacement, string expectedOutput, bool expectChange)
        {
            bool didChange = RegexUtil.ReplaceFirstOccurence(ref input, pattern, replacement);

            Assert.Equal(expectChange, didChange);
            Assert.Equal(expectedOutput, input);
        }
    }
}
