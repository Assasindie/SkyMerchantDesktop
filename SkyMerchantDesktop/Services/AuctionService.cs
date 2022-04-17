using System.Collections.Generic;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using System.Linq;

namespace SkyMerchantDesktop.Services
{
    public class AuctionService : IAuctionService
    {
        public List<Auction> FilterAuctionsByUser(string uuid, List<Auction> auctions)
        {
            return auctions.FindAll(o => o.uuid == uuid);
        }
    }
}