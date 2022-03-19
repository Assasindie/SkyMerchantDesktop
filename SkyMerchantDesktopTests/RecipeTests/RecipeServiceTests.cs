using SkyMerchantDesktop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop;
using SkyMerchantDesktop.Exceptions;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktopTests.RecipeTests
{
    internal class RecipeServiceTests : BaseUnitTest
    {
        protected IBazaarAPIService _bazaarAPIService;
        protected IRecipeService _recipeService;
        protected List<Bazaar> _bazaar;
        protected List<RecipeItem> _recipes;
        protected IRecipeAPIService _recipeApiService;
        protected List<Auction> _auctions;
        protected IAuctionService _auctionService;

        [OneTimeSetUp]
        protected override void OneTimeSetup()
        {
            base.OneTimeSetup();
            _recipeService = new RecipeService();
            _bazaarAPIService = new BazaarAPITestService();
            _recipeApiService = new RecipeAPITestService();
            _auctionService = new AuctionAPITestService();
        }
            
        [SetUp]
        public async Task SetUp()
        {
            _bazaar = await _bazaarAPIService.GetAllBazaarItems();
            _recipes = await _recipeApiService.GetLatestRecipes();
            _auctions = await _auctionService.GetAllBINAuctions();
        }

        [Test]
        [TestCase("Bazaar1", 5, 50.5)]
        [TestCase("Bazaar3",5,67.5)]
        public async Task GetRecipeSlotCost(string name, int count, decimal expectedCost)
        {
            decimal cost = _recipeService.GetCostForRecipeSlot(name, count, _bazaar, _auctions);
            Assert.AreEqual(expectedCost,cost);
        }
        
        [Test]
        public async Task GetRecipeSlotThrowNotFoundException()
        {
            Assert.Throws<RecipeItemNotFoundException>(() =>
            {
                _recipeService.GetCostForRecipeSlot("NotItem", 0, _bazaar, _auctions);
            });
        }
        
        
        [Test]
        public async Task GetRecipeSlotThrowNotEnoughException()
        {
            Assert.Throws<RecipeNotEnoughItemsException>(() =>
            {
                _recipeService.GetCostForRecipeSlot("Bazaar3", 50, _bazaar, _auctions);
            });
        }

        [Test]
        [TestCase("Recipe1")]
        public void GetRecipeByName(string name)
        {
            Assert.NotNull(_recipeService.GetRecipeByName(name, _recipes));
        }

        [Test]
        [TestCase("NonExistRecipe")]
        public void GetRecipeByNameMissing(string name)
        {
            Assert.Null(_recipeService.GetRecipeByName(name, _recipes));
        }
        
        [Test]
        [TestCase("Recipe1",30.3)]
        [TestCase("Recipe2", 494.9)]
        [TestCase("Recipe3",572.5)]
        public async Task GetRecipeCost(string recipeName, decimal expectedCost)
        {
            RecipeItem? item = _recipeService.GetRecipeByName(recipeName, _recipes);
            decimal cost = _recipeService.GetCostForRecipe(item?.recipe, _bazaar, _auctions);
            Assert.AreEqual(expectedCost, cost);
        }

        [Test]
        [TestCase("RecipeNotEnough")]
        public void GetRecipeCostNotEnough(string recipeName)
        {
            RecipeItem? item = _recipeService.GetRecipeByName(recipeName, _recipes); 
            Assert.AreEqual(-1, _recipeService.GetCostForRecipe(item?.recipe, _bazaar, _auctions));
        }

        [Test]
        public void GetRecipeFilteredList()
        {
            List<RecipeItem> items = _recipeService.FilterByItemsWithRecipe(_recipes);
            //assert some items have been removed
            Assert.True(items.Count != _recipes.Count);
        }

        [Test]
        [TestCase(1,572.5,12000000)]
        public void GetRecipeListWithCosts(int expectedCount, decimal expectedBazaarCost, decimal expectedAuctionCost)
        {
            List<RecipeItem> items = _recipeService.GetRecipeListWithCosts(_recipes, _auctions, _bazaar);
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(expectedBazaarCost, items.First().bazaarCost);
            Assert.AreEqual(expectedAuctionCost, items.First().lowestAuction);
        }
    }
}
