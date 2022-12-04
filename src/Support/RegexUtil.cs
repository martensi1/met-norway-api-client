using System.Text.RegularExpressions;

namespace PilotAppLib.Clients.MetNorway.Support
{
    internal static class RegexUtil
    {
        public static bool ReplaceFirstOccurence(ref string input, string pattern, string replacement)
        {
            var regex = new Regex(pattern);
            string result = regex.Replace(input, replacement, 1);

            if (!result.Equals(input))
            {
                input = result;
                return true;
            }

            return false;
        }
    }
}
