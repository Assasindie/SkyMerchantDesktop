using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktopTests.RecipeTests
{
    public class RecipeServiceTestRealData : BaseUnitTest 
    {
        protected IBazaarAPIService _bazaarAPIService;
        protected IRecipeService _recipeService;
        protected List<Bazaar> _bazaar;
        protected List<RecipeItem> _recipes;
        protected IRecipeAPIService _recipeApiService;
        protected List<Auction> _auctions;
        protected IAuctionAPIService AuctionApiService;
        
        [OneTimeSetUp]
        protected override void OneTimeSetup()
        {
            base.OneTimeSetup();
            _recipeService = new RecipeService();
            this.AuctionApiService = new AuctionApiService();
            this._recipeApiService = new RecipeAPIService();
            this._bazaarAPIService = new BazaarAPIService();
        }
        
        [SetUp]
        public new async Task SetUp()
        {
            this._recipes = await _recipeApiService.GetLatestRecipes();
            _recipes = _recipeService.FilterByItemsWithRecipe(_recipes);
            this._auctions = await AuctionApiService.GetAllBINAuctions();
            this._bazaar = await _bazaarAPIService.GetAllBazaarItems();
        }
        
        [Test]
        public async Task GetRecipeListWithCostsRealData()
        {
            List<RecipeItem> items = _recipeService.GetRecipeListWithCosts(_recipes, _auctions, _bazaar);
            Assert.IsTrue(items.Count > 0);
        }
    }
}