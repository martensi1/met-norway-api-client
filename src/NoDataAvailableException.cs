using System;

namespace PilotAppLib.Clients.MetNorway
{
    /// <summary>
    /// Is thrown if no data is available for the specified airport
    /// </summary>
    public sealed class NoDataAvailableException : Exception
    {
        /// <summary>
        /// Airport ICAO
        /// </summary>
        public string Airport { get; private set; }

        /// <summary>
        /// Type of data that was not available
        /// </summary>
        public string DataType { get; private set; }


        /// <summary>Initializes a new instance of the <see cref="NoDataAvailableException" /> class</summary>
        /// <param name="airport">Airport ICAO</param>
        /// <param name="dataType">Type of data that was not available</param>
        public NoDataAvailableException(string airport, string dataType)
            : base($"{dataType} missing for airport {airport}")
        {
            Airport = airport;
            DataType = dataType;
        }
    }
}