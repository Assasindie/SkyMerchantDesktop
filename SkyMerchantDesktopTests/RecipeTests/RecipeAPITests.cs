using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;

namespace SkyMerchantDesktopTests.RecipeTests
{
    internal class RecipeAPITests : BaseUnitTest
    {
        private IRecipeAPIService _recipeApiService;

        [SetUp]
        public void SetUp()
        {
            _recipeApiService = new RecipeAPIService();
        }

        [Test]
        public async Task GetLatestRecipes()
        {
            List<RecipeItem> items = await _recipeApiService.GetLatestRecipes();
            Assert.IsNotEmpty(items);
        }
    }
}