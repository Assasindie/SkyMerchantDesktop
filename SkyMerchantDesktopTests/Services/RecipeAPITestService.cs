using System.Collections.Generic;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;

namespace SkyMerchantDesktopTests.Services
{
    public class RecipeAPITestService : IRecipeAPIService
    {
        public async Task<List<RecipeItem>> GetLatestRecipes()
        {
            return new List<RecipeItem>()
            {
                new RecipeItem(){
                    name = "Recipe1",
                    recipe = new Recipe()
                    {
                        A1 = "Bazaar1:1",
                        A2 = "Bazaar2:1",
                        A3 = "Bazaar3:1"
                    },
                },
                new RecipeItem(){
                    name = "Recipe2",
                    recipe = new Recipe()
                    {
                        A1 = "Bazaar1:1",
                        B2 = "Bazaar1:16",
                        C3 = "Bazaar1:32"
                    },
                },
                new RecipeItem(){
                    name = "Recipe3",
                    recipe = new Recipe()
                    {
                        A1 = "Bazaar1:1",
                        C3 = "Bazaar1:32",
                        B2 = "Bazaar1:16",
                        A2 = "Bazaar2:1", 
                        C2 = "Bazaar3:5",
                    },
                },
                new RecipeItem()
                {
                    name = "SOUL_WHIP",
                    recipe = new Recipe()
                    {
                        A1 = "Bazaar1:1",
                        C3 = "Bazaar1:32",
                        B2 = "Bazaar1:16",
                        A2 = "Bazaar2:1", 
                        C2 = "Bazaar3:5",
                    },
                },
                new RecipeItem(){
                    name = "RecipeNotEnough",
                    recipe = new Recipe()
                    {
                        A1 = "Bazaar3:16",
                        A2 = "Bazaar3:1",
                        A3 = "Bazaar3:1"
                    },
                },
                new RecipeItem()
                {
                    name = "NoRecipe"
                }
            };
        }

        public void DownloadLatestRecipes()
        {
            throw new System.NotImplementedException();
        }

        public RecipeItem GetRecipe(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<RecipeItem>> LoadRecipes()
        {
            throw new System.NotImplementedException();
        }
    }
}