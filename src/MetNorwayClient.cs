using System;
using System.Text.RegularExpressions;

namespace PilotAppLib.Clients.MetNorway
{
    /// <summary>
    /// Client for fetching TAF and METAR from the MET Norway API
    /// </summary>
    public class MetNorwayClient : IDisposable
    {
        private readonly IApiClient _apiClient;


        /// <summary>Initializes a new instance of the <see cref="MetNorwayClient" /> class</summary>
        public MetNorwayClient() : this(
            new ApiClient(
                new EndpointBuilder(),
                new HttpGateway(),
                new ResponseProcessor()
            ))
        {
        }

        internal MetNorwayClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>Releases the unmanaged resources and disposes of the managed resources used by <see cref="MetNorwayClient" /></summary>
        public void Dispose()
        {
            _apiClient.Dispose();
        }


        /// <summary>Retrieves the latest METAR for the specified airport</summary>
        /// <param name="airport">Airport ICAO code</param>
        /// <returns>Airport METAR</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="airport"/> is <c>null</c></exception>
        /// <exception cref="@System.ArgumentException">The value of <paramref name="airport"/> is not a valid ICAO airport identifier</exception>
        /// <exception xref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout</exception>
        /// <exception cref="NoDataAvailableException">The response was empty (no report available)</exception>
        public string FetchMetar(string airport)
        {
            CheckAirportIcao(airport, nameof(airport));
            return _apiClient.GetReport(airport, "METAR");
        }

        /// <summary>Retrieves the latest TAF for the specified airport</summary>
        /// <param name="airport">Airport ICAO code</param>
        /// <returns>Airport TAF</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="airport"/> is <c>null</c></exception>
        /// <exception cref="System.ArgumentException">The value of <paramref name="airport"/> is not a valid ICAO airport identifier</exception>
        /// <exception cref="System.Net.Http.HttpRequestException">The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout</exception>
        /// <exception cref="NoDataAvailableException">The response was empty (no report available)</exception>
        public string FetchTaf(string airport)
        {
            CheckAirportIcao(airport, nameof(airport));
            return _apiClient.GetReport(airport, "TAF");
        }


        private void CheckAirportIcao(string airportIcao, string parameterName)
        {
            if (airportIcao == null)
            {
                throw new ArgumentNullException(airportIcao);
            }

            if (!Regex.IsMatch(airportIcao, @"^[A-Za-z]{4}$"))
            {
                throw new ArgumentException($"{airportIcao} is not a valid ICAO identifier", parameterName);
            }
        }
    }
}