using System;
using System.Linq;

namespace PilotAppLib.Clients.MetNorway
{
    interface IEndpointBuilder
    {
        public string BuildHttpEndpoint(string dataType, string airportIcao);
    }

    class EndpointBuilder : IEndpointBuilder
    {
        private const string BaseUrl = "https://api.met.no/weatherapi/tafmetar/1.0";

        private readonly string[] ValidDataTypes = {
            "METAR",
            "TAF"
        };


        public string BuildHttpEndpoint(string dataType, string airportIcao)
        {
            ThrowIfInvalidDataType(dataType, nameof(dataType));

            string httpEndpoint = BaseUrl
                    + $"/{dataType.ToLower()}"
                    + $"?icao={airportIcao}";

            return httpEndpoint;
        }


        private void ThrowIfInvalidDataType(string reportType, string parameterName)
        {
            if (reportType == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            if (!ValidDataTypes.Contains(reportType))
            {
                throw new ArgumentException("Invalid report type", parameterName);
            }
        }
    }
}
