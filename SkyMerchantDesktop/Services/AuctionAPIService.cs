using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;

namespace SkyMerchantDesktop.Services
{
    public class AuctionApiService : IAuctionAPIService
    {
        public async Task<List<Auction>> GetAllBINAuctions()
        {
            AuctionPage page = await App.APIRequestService.MakeSkyMerchantRequest<AuctionPage>("Auctions","GET");
            //api doesnt have filter on getting all auctions for bin only, would have to be paginated requests which dont want.
            List<Auction> auctions = page.auctions.Where(o => o.bin && !string.IsNullOrEmpty(o.itemName)).ToList();
            auctions.Sort();
            return auctions;
        }
    }
}