using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public class BlobContainerClient : IBlobContainerClient
    {
        private readonly HttpClient _client;

        private readonly ILogger<BlobContainerClient> _logger;

        public BlobContainerClient(
            HttpClient client,
            ILogger<BlobContainerClient> logger)
        {
            client.DefaultRequestHeaders.Add("User-Agent", "IpfyClient");
            client.DefaultRequestHeaders.Add("x-ms-blob-type", "BlockBlob");

            _client = client;
            _logger = logger ?? new NullLogger<BlobContainerClient>();
        }

        public async Task<string> PutIpAddress(string url, string ip)
        {
            var response = await _client.PutAsync(url, new StringContent($"{ip} {DateTime.Now}"));
            _logger.LogDebug($"{nameof(BlobContainerClient)} status code: {response.StatusCode}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
