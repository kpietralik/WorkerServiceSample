using System;
using System.Threading.Tasks;
using WorkerServiceSample;

namespace WorkerServiceSampleUnitTests
{
    public class IpfyClientMock : IIpfyClient
    {
        ////public HttpClient Client { get; }

        ////public IpfyClientMock(HttpClient client)
        ////{
        ////    //client.BaseAddress = new Uri("https://api.ipify.org?format=json");
        ////    client.BaseAddress = new Uri("https://api.ipify.org");
            
        ////    client.DefaultRequestHeaders.Add("User-Agent", "IpfyClient");

        ////    Client = client;
        ////}

        public Task<string> GetIpAddress()
        {
            throw new ArgumentOutOfRangeException("TEST"); // TEMP, for tests
        }
    }
}
