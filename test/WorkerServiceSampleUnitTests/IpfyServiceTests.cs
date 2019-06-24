using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using WorkerServiceSample;
using Xunit;

namespace WorkerServiceSampleUnitTests
{
    public class IpfyServiceTests
    {
        [Fact]
        public async Task Test1()
        {
            var mockClient = new IpfyClientMock();
            var settings = new Settings();
            var sut = new IpfyService(Options.Create(settings) , mockClient, null, null);

            Func<Task> act = async () => await sut.StartAsync(new System.Threading.CancellationToken());

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
