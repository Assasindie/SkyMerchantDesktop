using System.Collections.Generic;

namespace SkyMerchantDesktop.Models
{
    public class AuctionPage
    {
        public bool success { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
        public int totalAuctions { get; set; }
        public List<Auction> auctions { get; set; }
    }
}