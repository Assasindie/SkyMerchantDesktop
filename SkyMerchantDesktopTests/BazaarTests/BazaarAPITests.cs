using NUnit.Framework;
using SkyMerchantDesktop;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Models.Interfaces;
using System.Threading.Tasks;

namespace SkyMerchantDesktopTests
{
    public class BazaarAPITests : BaseUnitTest
    {
        private IBazaarAPIService bazaarAPIService;

        [SetUp]
        public void Setup()
        {
            bazaarAPIService = new BazaarAPIService();
        }

        [Test]
        public async Task GetAllBazaarItemsTest()
        {
            var results = await bazaarAPIService.GetAllBazaarItems();
            Assert.IsTrue(results.Count > 0);
        }
    }
}