using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IAuctionService
    {
        public Task<List<Auction>> GetAllBINAuctions();
    }
}