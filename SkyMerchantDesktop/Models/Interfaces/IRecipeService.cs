using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Recipe;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IRecipeService
    {
        public decimal GetCostForRecipe(Recipe.Recipe? recipe, List<Bazaar> bazzar, List<Auction> auctions);
        public decimal GetCostForRecipeSlot(string name, int count, List<Bazaar> bazaar, List<Auction> auctions);
        public RecipeItem? GetRecipeByName(string name, List<RecipeItem> items);
        
        public List<RecipeItem> FilterByItemsWithRecipe(List<RecipeItem> items);

        public List<RecipeItem> GetRecipeListWithCosts(List<RecipeItem> items, List<Auction> auctions,
            List<Bazaar> bazaar);
    }
}
