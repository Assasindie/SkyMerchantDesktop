using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SkyMerchantDesktop.Models;

namespace SkyMerchantDesktop.Utils
{
    public class AuctionUtils
    {
        public static List<Auction> FindByAuctionName(string name, List<Auction> auctions)
        {
            string formattedName = name.ToLower().Replace("_", " ");
            List<Auction> existingAuctions = auctions.FindAll(o => o.itemName.ToLower().Contains(formattedName));
            return existingAuctions;
        }

        public static decimal GetLowestAuctionByName(string name, List<Auction> auctions)
        {
            List<Auction> filteredAuctions = FindByAuctionName(name, auctions);
            if (filteredAuctions.Count == 0) return -1;
            filteredAuctions.Sort();
            return filteredAuctions.First().bid;
        }
    }
}