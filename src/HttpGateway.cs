using PilotAppLib.Http;
using System;

namespace PilotAppLib.Clients.MetNorway
{
    interface IHttpGateway : IDisposable
    {
        public string SendRequest(string endpoint);
    }

    class HttpGateway : IHttpGateway
    {
        private readonly SimpleHttp _httpClient;


        public HttpGateway()
        {
            _httpClient = new SimpleHttp();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }


        public string SendRequest(string endpoint)
        {
            SimpleHttp.HttpResponse response = _httpClient.Get(endpoint, "application/text");
            return response.AsString();
        }
    }
}
