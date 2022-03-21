using SkyMerchantDesktop.Models.Recipe;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SkyMerchantDesktop.Utils
{
    public class RecipeUtils
    {
        //convert "GIANT_FRAGMENT_BOULDER:1" to "GIANT_FRAGMENT_BOULDER", 1
        public static Tuple<string,int> GetQuantityNameFromRecipe(string item)
        {
            //match the number part
            string regex = Regex.Match(item, ":[0-9]*").Value;
            //remove number part from name
            string name = item.Replace(regex, "");
            //remove colon and parse to int
            int total = int.Parse(regex.Replace(":",""));
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

        public static VisualRecipe TransformRecipeToVisualRecipe(Recipe recipe)
        {
            VisualRecipe visualRecipe = new VisualRecipe();
            PropertyInfo[] RecipeInfos = typeof(Recipe).GetProperties();
            PropertyInfo[] VisualRecipeInfos = typeof(VisualRecipe).GetProperties();
            for (int i = 0; i < VisualRecipeInfos.Length; i++)
            {
                //holy unsafe but looks cleaner
                string value = (string) RecipeInfos[i].GetValue(recipe);
                if (string.IsNullOrEmpty(value)) continue ;
                Tuple<string, int> quantityName = GetQuantityNameFromRecipe(value);
                VisualRecipeInfos[i].SetValue(visualRecipe, new RecipeSlotItem() { Name = FormatCapitalSnakeCase(quantityName.Item1), Quantity = quantityName.Item2 });
            }
            /*

            quantityName = GetQuantityNameFromRecipe(recipe.A1);
            visualRecipe.A1 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.A2);
            visualRecipe.A2 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.A3);
            visualRecipe.A3 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.B1);
            visualRecipe.B1 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.B2);
            visualRecipe.B2 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.B3);
            visualRecipe.B3 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.C1);
            visualRecipe.C1 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.C2);
            visualRecipe.C2 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };

            quantityName = GetQuantityNameFromRecipe(recipe.C3);
            visualRecipe.C3 = new() { Name = quantityName.Item1, Quantity = quantityName.Item2 };
            */


            return visualRecipe;
        }
    }
}