using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IAuctionAPIService
    {
        public Task<List<Auction>> GetAllBINAuctions();
    }
}