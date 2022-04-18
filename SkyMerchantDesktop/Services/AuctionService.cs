using System.Collections.Generic;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using System.Linq;

namespace SkyMerchantDesktop.Services
{
    public class AuctionService : IAuctionService
    {
        public List<Auction> FilterAuctionsByUser(string auctioneer, List<Auction> auctions)
        {
            if (string.IsNullOrEmpty(auctioneer))
            {
                return new List<Auction>();
            }
            return auctions.FindAll(o => o.auctioneer == auctioneer);
        }
    }
}