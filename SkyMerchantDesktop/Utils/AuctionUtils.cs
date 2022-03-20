using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SkyMerchantDesktop.Models;

namespace SkyMerchantDesktop.Utils
{
    public class AuctionUtils
    {
        public static List<Auction> FindByAuctionName(string name, List<Auction> auctions)
        {
            string formattedName = name.ToLower().Replace("_", " ");
            List<Auction> existingAuctions = auctions.FindAll(o => o.itemName.ToLower().Contains(formattedName));
            return existingAuctions;
        }
        
        public static List<Auction> FindAuctionByRecipeNamesAndDeepClone(string A1, string A2, string A3, 
            string B1, string B2, string B3,string C1, string C2, string C3, List<Auction> auctions)
        {
            //i love this - someday will make this nicer
            A1 = RecipeUtils.GetNameFromRecipe(A1?.ToLower().Replace("_", " "));
            A2 = RecipeUtils.GetNameFromRecipe(A2?.ToLower().Replace("_", " "));
            A3 = RecipeUtils.GetNameFromRecipe(A3?.ToLower().Replace("_", " "));
            B1 = RecipeUtils.GetNameFromRecipe(B1?.ToLower().Replace("_", " "));
            B2 = RecipeUtils.GetNameFromRecipe(B2?.ToLower().Replace("_", " "));
            B3 = RecipeUtils.GetNameFromRecipe(B3?.ToLower().Replace("_", " "));
            C1 = RecipeUtils.GetNameFromRecipe(C1?.ToLower().Replace("_", " "));
            C2 = RecipeUtils.GetNameFromRecipe(C2?.ToLower().Replace("_", " "));
            C3 = RecipeUtils.GetNameFromRecipe(C3?.ToLower().Replace("_", " "));

            List<string> names = new() {A1, A2, A3, B1, B2, B3, C1, C2, C3};
            List<Auction> existingAuctions = auctions.FindAll(o => names.Contains(o.itemName.ToLower()));
            //deep clone it so can remove from list without affecting main auction list
            return JsonConvert.DeserializeObject<List<Auction>>(JsonConvert.SerializeObject(existingAuctions))!;
        }

        public static decimal GetLowestAuctionByName(string name, List<Auction> auctions)
        {
            List<Auction> filteredAuctions = FindByAuctionName(name, auctions);
            if (filteredAuctions.Count == 0) return -1;
            filteredAuctions.Sort();
            return filteredAuctions.First().bid;
        }
    }
}