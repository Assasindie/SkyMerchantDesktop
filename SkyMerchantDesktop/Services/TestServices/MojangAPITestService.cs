using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Interfaces;

namespace SkyMerchantDesktopTests.Services
{
    public class MojangAPITestService : IMojangAPIService
    {
        public async Task<string> GetUUIDFromUsername(string username)
        {
            return "73";
        }
    }
}