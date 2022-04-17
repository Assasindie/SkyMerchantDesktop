using System.Threading.Tasks;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IMojangAPIService
    {
        public Task<string> GetUUIDFromUsername(string username);
    }
}