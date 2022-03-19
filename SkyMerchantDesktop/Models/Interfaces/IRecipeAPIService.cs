using System.Collections.Generic;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IRecipeAPIService
    {
        public Task<List<RecipeItem>> GetLatestRecipes();

        public void DownloadLatestRecipes();

        public RecipeItem GetRecipe(string name);

        public Task<List<RecipeItem>> LoadRecipes();
    }
}