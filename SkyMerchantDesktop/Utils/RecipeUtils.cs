using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Services;

namespace SkyMerchantDesktop.Utils
{
    public class RecipeUtils
    {
        //convert "GIANT_FRAGMENT_BOULDER:1" to "GIANT_FRAGMENT_BOULDER", 1
        public static Tuple<string, int> GetQuantityNameFromRecipe(string item)
        {
            //match the number part
            string regex = Regex.Match(item, ":[0-9]*").Value;
            //remove number part from name
            string name = item.Replace(regex, "");
            //remove colon and parse to int
            int total = int.Parse(regex.Replace(":", ""));
            return new Tuple<string, int>(name, total);
        }

        public static string GetNameFromRecipe(string? item)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                return string.Empty;
            }
            //match the number part
            string regex = Regex.Match(item, ":[0-9]*").Value;
            //remove number part from name
            return item.Replace(regex, "");
        }

        public static string FormatCapitalSnakeCase(string name)
        {
            return name.Replace("_", " ").ToLower();
        }

        public static VisualRecipe TransformRecipeToVisualRecipe(Recipe recipe, IRecipeService service, List<Bazaar> bazaar, List<Auction> auctions)
        {
            VisualRecipe visualRecipe = new VisualRecipe();
            PropertyInfo[] recipeInfos = typeof(Recipe).GetProperties();
            PropertyInfo[] visualRecipeInfos = typeof(VisualRecipe).GetProperties();
            List<RecipeSlotItem> uniqueRecipeSlotItems = new();
            //reflection magic to turn Recipe into Visual Recipe and get the Quantity/Readble name
            //length - 1 as the final object the List<RecipeItem> doesnt exist in Recipe
            for (int i = 0; i < visualRecipeInfos.Length - 1; i++)
            {
                string value = (string)recipeInfos[i].GetValue(recipe);
                if (string.IsNullOrEmpty(value)) continue;
                Tuple<string, int> quantityName = GetQuantityNameFromRecipe(value);
                string formattedName = FormatCapitalSnakeCase(quantityName.Item1);
                decimal cost = service.GetCostForRecipeSlot(quantityName.Item1, quantityName.Item2, bazaar, auctions);
                RecipeSlotItem item = new()
                {
                    Name = formattedName,
                    Quantity = quantityName.Item2,
                    Cost = cost
                };
                visualRecipeInfos[i].SetValue(visualRecipe, item);
                //see if we need to add it to the list
                RecipeSlotItem? existingRecipeSlotItem = uniqueRecipeSlotItems.FirstOrDefault(o => o.Name == formattedName, null);
                if (existingRecipeSlotItem == null)
                {
                    uniqueRecipeSlotItems.Add(new RecipeSlotItem() { Quantity = quantityName.Item2, Cost = cost, Name = formattedName });
                }
                else
                {
                    existingRecipeSlotItem.Cost += cost;
                    existingRecipeSlotItem.Quantity += quantityName.Item2;
                }
            }

            visualRecipe.Items = uniqueRecipeSlotItems;
            return visualRecipe;
        }
    }
}