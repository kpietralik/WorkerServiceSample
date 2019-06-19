using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public class BlobContainerClient
    {
        public HttpClient Client { get; }

        public BlobContainerClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "IpfyClient");
            client.DefaultRequestHeaders.Add("x-ms-blob-type", "BlockBlob");

            Client = client;
        }

        public async Task<string> PutIpAddress(string url, string ip)
        {
            var response = await Client.PutAsync(url, new StringContent($"{ip} {DateTime.Now}"));

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
