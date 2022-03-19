using System;
using NUnit.Framework;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;

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
    }
}