using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;
using SkyMerchantDesktopTests.Services;
using System.Linq;

namespace SkyMerchantDesktopTests.RecipeTests
{
    public class RecipeUtilTest
    {
        [Test]
        [TestCase("ACACIA_GENERATOR_8:3","ACACIA_GENERATOR_8",3)]
        [TestCase("WOOD-4:1","WOOD-4",1)]
        [TestCase("ENCHANTED_ACACIA_LOG:16","ENCHANTED_ACACIA_LOG",16)]
        [TestCase("NECRON_BLADE:64","NECRON_BLADE",64)]
        public void GetQuantityFromRecipeString(string name, string expectedName, int exceptedTotalValue)
        {
            Tuple<string,int> res = RecipeUtils.GetQuantityNameFromRecipe(name);
            Assert.AreEqual(exceptedTotalValue, res.Item2);
            Assert.AreEqual(expectedName, res.Item1);
        }


        [Test]
        [TestCase("Recipe3","bazaar1", 1, "bazaar1", 32)]
        public async Task TransformRecipeToVisualRecipe(string RecipeName, string ExpectedFirstName, int ExpectedFirstQuantity, string ExpectedLastName, int ExpectedLastQuantity)
        {
             IRecipeAPIService service = new RecipeAPITestService();
            List<RecipeItem> recipes = await service.GetLatestRecipes();
            RecipeItem recipe = recipes.FirstOrDefault(o => o.name == RecipeName, null);
            //if this throws check your recipename is valid
            if(recipe == null) { throw new Exception(); }
            VisualRecipe visualRecipe = RecipeUtils.TransformRecipeToVisualRecipe(recipe.recipe);
            Assert.IsTrue(visualRecipe.A1 != null && visualRecipe.A1.Name == ExpectedFirstName && visualRecipe.A1.Quantity == ExpectedFirstQuantity);
            Assert.IsTrue(visualRecipe.C3 != null && visualRecipe.C3.Name == ExpectedFirstName && visualRecipe.C3.Quantity == ExpectedFirstQuantity);

        }
    }
}