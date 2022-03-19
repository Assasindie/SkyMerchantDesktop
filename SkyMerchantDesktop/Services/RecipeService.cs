using SkyMerchantDesktop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using SkyMerchantDesktop.Exceptions;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Utils;

namespace SkyMerchantDesktop.Services
{
    public class RecipeService : IRecipeService
    {
        public decimal GetCostForRecipe(Recipe? recipe, List<Bazaar> bazzar, List<Auction> auctions)
        {
            if (recipe == null) throw new Exception();
            decimal totalCost = 0;
            try
            {
                totalCost += CheckNotNUllAndGetCost(recipe.A1, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.A2, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.A3, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.B1, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.B2, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.B3, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.C1, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.C2, bazzar, auctions);
                totalCost += CheckNotNUllAndGetCost(recipe.C3, bazzar, auctions);
                return totalCost;
            }catch (RecipeNotEnoughItemsException e)
            {
                return -1;
            } catch (RecipeItemNotFoundException e)
            {
                return -1;
            }
        }

        private decimal CheckNotNUllAndGetCost(string name, List<Bazaar> bazaar, List<Auction> auctions)
        {
            if (string.IsNullOrWhiteSpace(name)) return 0;
            Tuple<string,int> result = RecipeUtils.GetQuantityNameFromRecipe(name);
            return GetCostForRecipeSlot(result.Item1, result.Item2, bazaar, auctions);
        }

        public decimal GetCostForRecipeSlot(string name, int count, List<Bazaar> bazaar, List<Auction> auctions)
        {
            decimal cost = 0;
            Bazaar? item = bazaar.Find(o => o.product_id == name);
            if (item == null)
            {
                //if item cant be found on the bazaar it might be an upgrade recipe so search the AH for the cheapest version.
                List<Auction> filteredAuctions = AuctionUtils.FindByAuctionName(name, auctions);
                if (filteredAuctions.Count == 0) throw new RecipeItemNotFoundException();
                if(filteredAuctions.Count < count) throw new RecipeNotEnoughItemsException();
                for (int i = 0; i < count; i++)
                {
                    cost += filteredAuctions[i].bid;
                }
                return cost;
            }
            if (item.buy_summary != null)
                foreach (BazaarCostSummary bid in item.buy_summary)
                {
                    if (count <= 0) break;
                    //total number to be subtracted shouldnt be higher than count.
                    int total = bid.amount > count ? count : bid.amount;
                    cost += bid.pricePerUnit * total;
                    count -= total;
                }

            if (count > 0)
            {
                throw new RecipeNotEnoughItemsException();
            }
            return cost;
        }

        public RecipeItem? GetRecipeByName(string name, List<RecipeItem> items)
        {
            return items.FirstOrDefault(o => o?.name == name, null);
        }

        public List<RecipeItem> FilterByItemsWithRecipe(List<RecipeItem> items)
        {
            return items.FindAll(o => o.recipe != null);
        }
        
        public List<RecipeItem> GetRecipeListWithCosts(List<RecipeItem> items, List<Auction> auctions, List<Bazaar> bazaar)
        {
            List<RecipeItem> filtered = new();
            foreach (RecipeItem item in items)
            {
                decimal lowestAuction = AuctionUtils.GetLowestAuctionByName(item.name, auctions);
                if (lowestAuction == -1) continue;

                item.lowestAuction = lowestAuction;
                //add bazaar cost
                decimal cost = GetCostForRecipe(item.recipe, bazaar, auctions);
                if (cost == -1) continue;
                
                item.bazaarCost = cost;
                    
                filtered.Add(item);
            }
            return filtered; 
        }
    }
}
