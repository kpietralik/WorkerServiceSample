using System;
using System.Threading.Tasks;
using WorkerServiceSample;

namespace WorkerServiceSampleUnitTests
{
    public class BlobContainerClientMoct : IBlobContainerClient
    {
        public Task<string> PutIpAddress(string url, string ip)
        {
            throw new ArgumentOutOfRangeException("TEST"); // TEMP, for tests
        }
    }
}
