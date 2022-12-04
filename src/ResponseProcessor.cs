using PilotAppLib.Clients.MetNorway.Support;
using System.Linq;

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
            result = ReorderAutoModifier(result);

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

        private string ReorderIcaoCode(string reportString, string aerodromeCode)
        {
            aerodromeCode = aerodromeCode.ToUpper();
            var pattern = @"( " + aerodromeCode + "(?= \\d{6}Z )|^" + aerodromeCode + " )";

            if (RegexUtil.ReplaceFirstOccurence(ref reportString, pattern, string.Empty))
            {
                // Only reorder if the ICAO code was found
                reportString = aerodromeCode + " " + reportString;
            }

            return reportString;
        }

        private string ReorderAutoModifier(string reportString)
        {
            if (RegexUtil.ReplaceFirstOccurence(ref reportString, @"( AUTO )", " "))
            {
                // Only reorder if the AUTO modifier was found
                // Insert AUTO modifier behind the date and time of report
                _ = RegexUtil.ReplaceFirstOccurence(ref reportString, @"(?<=\d{6}Z) ", " AUTO ");
            }

            return reportString;
        }
    }
}
