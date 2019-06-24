using System.Threading.Tasks;

namespace WorkerServiceSample
{
    public interface IIpfyClient
    {
        public Task<string> GetIpAddress();
    }
}