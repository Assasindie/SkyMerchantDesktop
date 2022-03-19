using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;

namespace SkyMerchantDesktop.Services
{
    public class RecipeAPIService : IRecipeAPIService
    {
        //random github url i found that has all the recipes.
        private string mappingUrl =
            "https://raw.githubusercontent.com/kr45732/skyblock-plus-data/main/InternalNameMappings.json";
        public async Task<List<RecipeItem>> GetLatestRecipes()
        {
            //take response which is a bunch of objects with static names and turn into a list so can display and easily query.
            RecipeResponse response = await App.APIRequestService.MakeGenericRequest<RecipeResponse>(mappingUrl, "GET");
            List<RecipeItem> items = new List<RecipeItem>();
            PropertyInfo[] properties = typeof(RecipeResponse).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                //this foreach goes through every single variable in RecipeResponse which will be all the RecipeItems
                RecipeItem item = new();
                PropertyInfo[] itemProperties = info.GetValue(response)!.GetType().GetProperties();
                foreach (PropertyInfo property in itemProperties)
                {
                    //recreate the recipe item and add it to the list.
                    switch (property.Name)
                    {
                        case "name":
                            item.name = (string?) property.GetValue(info.GetValue(response));
                            break;
                        case "recipe":
                            item.recipe = (Recipe?) property.GetValue(info.GetValue(response));

                            break;
                        case "wiki":
                            item.wiki = (string?) property.GetValue(info.GetValue(response));

                            break;
                        default: break;
                    }
                }
                items.Add(item);
            }
            return items;
        }

        public void DownloadLatestRecipes()
        {
            //downloads json of latest recipes
            throw new System.NotImplementedException();
        }

        public RecipeItem GetRecipe(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<RecipeItem>> LoadRecipes()
        {
            throw new NotImplementedException();
        }
    }
}