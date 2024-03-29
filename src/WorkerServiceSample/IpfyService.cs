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
        private readonly IBlobContainerClient _blobContainerClient;

        public IpfyService(
            IOptions<Settings> settings,
            IIpfyClient ipfyClient,
            IBlobContainerClient blobContainerClient,
            ILogger<IpfyService> logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _ipfyClient = ipfyClient ?? throw new ArgumentNullException(nameof(ipfyClient));
            _blobContainerClient = blobContainerClient ?? throw new ArgumentNullException(nameof(blobContainerClient));
            _logger = logger ?? new NullLogger<IpfyService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker started at: {DateTimeOffset.Now}, " +
                $"with interval of: {_settings.Value.DelaySeconds} seconds, " +
                $"calling url: {_settings.Value.BlobContainerUrl}");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var time = DateTimeOffset.Now;

                    _logger.LogDebug($"Worker executing at: {time}");

                    var ip = await _ipfyClient.GetIpAddress();

                    _logger.LogDebug($"Ip = {ip}");

                    await _blobContainerClient.PutIpAddress(_settings.Value.BlobContainerUrl, ip);

                    await Task.Delay(TimeSpan.FromSeconds(_settings.Value.DelaySeconds), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
