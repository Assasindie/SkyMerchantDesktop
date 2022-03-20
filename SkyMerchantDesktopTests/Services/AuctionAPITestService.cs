using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;

namespace SkyMerchantDesktopTests.Services
{
    public class AuctionAPITestService : IAuctionService
    {
        public async Task<List<Auction>> GetAllBINAuctions()
        {
            return new List<Auction>()
            {
                new Auction()
                {
                    itemName = "Fast Soul Whip",
                    bin = true,
                    bid = 14999000,
                    count = 1
                },
                new Auction()
                {
                    itemName = "Withered Soul Whip",
                    bin = true,
                    bid = 12000000,
                    count = 1
                },
                new Auction()
                {
                    itemName = "NonExistItem",
                    bin = true,
                    bid = 14999000,
                    count = 1
                },
                new Auction()
                {
                    itemName = "Dragon Scale",
                    bin = true,
                    bid = 15000,
                    count = 1
                },
                new Auction()
                {
                    itemName = "Scythe Blade",
                    bin = true,
                    bid = 50000,
                    count = 1
                },
                new Auction()
                {
                    itemName = "Reaper Falchion",
                    bin = true,
                    bid = 1000000,
                    count = 2
                },
                new Auction()
                {
                    itemName = "Reaper Falchion",
                    bin = true,
                    bid = 1500000,
                    count = 2
                },
                new Auction()
                {
                    itemName = "Reaper Scythe",
                    bin = true,
                    bid = 5000000,
                    count = 1
                },
                new Auction()
                {
                itemName = "Reaper Scythe 2",
                bin = true,
                bid = 5000000,
                count = 1
            }
            };
        }
    }
}