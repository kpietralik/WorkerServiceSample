using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace WorkerServiceSample
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.SetBasePath(Directory.GetCurrentDirectory());
                configApp.AddJsonFile("appsettings.json", optional: true);
                configApp.AddJsonFile(
                    $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                    optional: true);
                configApp.AddEnvironmentVariables();
                configApp.AddUserSecrets<Settings>();
                configApp.AddCommandLine(args);
            })
            .ConfigureServices((context, services) =>
            {
                services
                    .AddOptions()
                    .Configure<Settings>(x => context.Configuration.GetSection(nameof(IpfyService)).Bind(x))
                    .AddIpfyClient()
                    .AddBlobContainerClient()
                    .AddHostedService<IpfyService>();
            })
            .ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder
                    .AddConsole()
                    .AddDebug()
                    .AddEventLog()
                    .AddFilter<EventLogLoggerProvider>($"{nameof(WorkerServiceSample)}.{nameof(IpfyService)}", LogLevel.Information);
    });
    }
}
