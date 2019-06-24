using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace WorkerServiceSample
{
    public class IpfyService : BackgroundService
    {
        private readonly ILogger<IpfyService> _logger;
        private readonly IOptions<Settings> _settings;
        private readonly IIpfyClient _ipfyClient;
        private readonly BlobContainerClient _blobContainerClient;
        
        public IpfyService(
            IOptions<Settings> settings,
            IIpfyClient ipfyClient,
            BlobContainerClient blobContainerClient,
            ILogger<IpfyService> logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _ipfyClient = ipfyClient ?? throw new ArgumentNullException(nameof(ipfyClient));
            // TEMP _blobContainerClient = blobContainerClient ?? throw new ArgumentNullException(nameof(blobContainerClient));
            _logger = logger ?? new NullLogger<IpfyService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker started at: {DateTimeOffset.Now}");

            while (!stoppingToken.IsCancellationRequested)
            {
                var time = DateTimeOffset.Now;

                _logger.LogInformation($"Worker {_settings.Value.BlobContainerUrl} running at: {time}");

                var ip = await _ipfyClient.GetIpAddress();

                _logger.LogInformation($"Worker IP = {ip} at: {time}");

                await _blobContainerClient.PutIpAddress(_settings.Value.BlobContainerUrl, ip);

                await Task.Delay(TimeSpan.FromMilliseconds(_settings.Value.DelaySeconds), stoppingToken);
            }
        }
    }
}
