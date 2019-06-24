using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public class IpfyClient : IIpfyClient
    {
        public HttpClient Client { get; }

        public IpfyClient(HttpClient client)
        {
            //client.BaseAddress = new Uri("https://api.ipify.org?format=json");
            client.BaseAddress = new Uri("https://api.ipify.org");
            
            client.DefaultRequestHeaders.Add("User-Agent", "IpfyClient");

            Client = client;
        }

        public async Task<string> GetIpAddress()
        {
            var response = await Client.GetAsync(string.Empty);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
