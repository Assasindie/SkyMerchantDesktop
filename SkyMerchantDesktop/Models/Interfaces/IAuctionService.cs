using System.Collections.Generic;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IAuctionService
    {
        public List<Auction> FilterAuctionsByUser(string uuid, List<Auction> auctions);
    }
}