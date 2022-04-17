using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktopTests.AuctionTests
{
    public class AuctionServiceTests : BaseUnitTest
    {
        private AuctionService _auctionService;
        private IAuctionAPIService _auctionApiService;
        private IMojangAPIService _mojangApiService;
        private List<Auction> _auctions;
        private string _uuid;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            base.OneTimeSetup();
            this._auctionApiService = new AuctionApiTestService();
            this._auctionService = new AuctionService();
            this._mojangApiService = new MojangAPITestService();
        }

        [SetUp]
        public async Task SetUp()
        {
            _auctions = await _auctionApiService.GetAllBINAuctions();
            this._uuid = await _mojangApiService.GetUUIDFromUsername("");
        }

        [Test]
        public void FilterAuctionsByUUID()
        {
            List<Auction> filteredAuctions = _auctionService.FilterAuctionsByUser(_uuid, _auctions);
            Assert.IsTrue(filteredAuctions.Count == 1);
        }
        
    }
}