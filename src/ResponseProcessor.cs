using System.Linq;

namespace PilotAppLib.Clients.MetNorway
{
    interface IResponseProcessor
    {
        public string Process(string responseText);
    }

    class ResponseProcessor : IResponseProcessor
    {
        public string Process(string responseText)
        {
            RemoveCarriageReturns(ref responseText);
            TrimLinebreaks(ref responseText);

            var lines = responseText.Split("\n");
            string last = lines.Last();

            return last.Trim();
        }


        private void RemoveCarriageReturns(ref string text)
        {
            text = text.Replace("\r", string.Empty);
        }

        private void TrimLinebreaks(ref string text)
        {
            text = text.Trim(' ', '\n');
        }
    }
}
