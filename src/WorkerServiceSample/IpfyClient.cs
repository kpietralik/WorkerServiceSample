using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public class IpfyClient : IIpfyClient
    {
        private readonly HttpClient _client;

        private readonly ILogger<IpfyClient> _logger;

        public IpfyClient(
            HttpClient client,
            ILogger<IpfyClient> logger)
        {
            //client.BaseAddress = new Uri("https://api.ipify.org?format=json");
            client.BaseAddress = new Uri("https://api.ipify.org");
            client.DefaultRequestHeaders.Add("User-Agent", "IpfyClient");

            _client = client;
            _logger = logger ?? new NullLogger<IpfyClient>();
        }

        public async Task<string> GetIpAddress()
        {
            var response = await _client.GetAsync(string.Empty);
            _logger.LogDebug($"{nameof(IpfyClient)} status code: {response.StatusCode}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
