using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktopTests.AuctionTests
{
    public class AuctionUtilTests : BaseUnitTest
    {
        protected List<Auction> _auctions;
        protected IAuctionService _auctionService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _auctionService = new AuctionAPITestService();
        }

        [SetUp]
        public async Task SetUp()
        {
            _auctions = await _auctionService.GetAllBINAuctions();
        }
        
        [Test]
        public void FindAuctionByRecipeNamesAndDeepClone()
        {
            List<Auction> auctions = AuctionUtils.FindAuctionByRecipeNamesAndDeepClone("SCYTHE_BLADE:1", "Revenant_Viscera:64",
                "Reaper_Falchion:1", "Reaper_Falchion:1", "Reaper_Falchion:1", null, null, null, null, _auctions);
            Assert.IsTrue(auctions.Count == 3);
        }
    }
}