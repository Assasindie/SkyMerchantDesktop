using System.Threading.Tasks;
using Newtonsoft.Json;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Mojang;

namespace SkyMerchantDesktop.Services
{
    public class MojangAPIService : IMojangAPIService
    {
        public async Task<string> GetUUIDFromUsername(string username)
        {
            MinecraftAccount response = await App.APIRequestService.MakeGenericRequest<MinecraftAccount>($"https://api.mojang.com/users/profiles/minecraft/{username}", "GET");
            return response.id;
        }
    }
}