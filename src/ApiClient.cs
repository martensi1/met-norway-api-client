using System;

namespace PilotAppLib.Clients.MetNorway
{
    interface IApiClient : IDisposable
    {
        public string GetReport(string airport, string reportType);
    }


    class ApiClient : IApiClient
    {
        private readonly IEndpointBuilder _endpointBuilder;
        private readonly IHttpGateway _httpGateway;
        private readonly IResponseProcessor _responseProcessor;


        public ApiClient(
            IEndpointBuilder endpointBuilder,
            IHttpGateway httpGateway,
            IResponseProcessor responseProcessor
            )
        {
            _endpointBuilder = endpointBuilder;
            _httpGateway = httpGateway;
            _responseProcessor = responseProcessor;
        }

        public void Dispose()
        {
            _httpGateway.Dispose();
        }


        public string GetReport(string airport, string reportType)
        {
            string text = SendRequest(airport, reportType);
            string responseData = _responseProcessor.Process(text);

            if (IsEmptyResponse(responseData))
            {
                throw new NoDataAvailableException(airport, reportType);
            }

            return responseData;
        }


        private string SendRequest(string location, string dataType)
        {
            string getEndpoint = _endpointBuilder.BuildHttpEndpoint(dataType, location);
            return _httpGateway.SendRequest(getEndpoint);
        }

        private bool IsEmptyResponse(string responseData)
        {
            return
                string.IsNullOrWhiteSpace(responseData) || 
                responseData.Equals("no-content");
        }
    }
}