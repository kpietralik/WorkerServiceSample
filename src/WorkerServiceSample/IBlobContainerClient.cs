using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public interface IBlobContainerClient
    {
        Task<string> PutIpAddress(string url, string ip);
    }
}