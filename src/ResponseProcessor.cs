using System.Linq;
using System.Text.RegularExpressions;

namespace PilotAppLib.Clients.MetNorway
{
    interface IResponseProcessor
    {
        public string Process(string responseText, string aerodromeCode);
    }

    class ResponseProcessor : IResponseProcessor
    {
        public string Process(string responseText, string aerodromeCode)
        {
            RemoveCarriageReturns(ref responseText);
            TrimLinebreaksAndWhitespace(ref responseText);

            var lines = responseText.Split("\n");

            string result = lines.Last().Trim();
            result = ReorderIcaoCode(result, aerodromeCode);

            return result;
        }

        private void RemoveCarriageReturns(ref string text)
        {
            text = text.Replace("\r", string.Empty);
        }

        private void TrimLinebreaksAndWhitespace(ref string text)
        {
            text = text.Trim(' ', '\n');
        }

        private string SplitAndGetTrimmedLastLine(string text)
        {
            var lines = text.Split("\n");
            string last = lines.Last();

            return last.Trim();
        }

        private string ReorderIcaoCode(string reportString, string aerodromCode)
        {
            aerodromCode = aerodromCode.ToUpper();

            var pattern = @"( " + aerodromCode + "(?= \\d{6}Z )|^" + aerodromCode + " )";
            string reorderedReport = Regex.Replace(reportString, pattern, string.Empty);

            if (!reorderedReport.Equals(reportString))
            {
                // Only reorder if the ICAO code was found
                reorderedReport = aerodromCode + " " + reorderedReport;
            }

            return reorderedReport;
        }
    }
}
