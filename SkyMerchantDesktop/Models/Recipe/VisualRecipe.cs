using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Models.Recipe
{
    //meant to be used by the BazaarViewModel
    public class VisualRecipe : BaseObservableModel
    {
        public RecipeSlotItem A1 { get; set; }
        public RecipeSlotItem A2 { get; set; }
        public RecipeSlotItem A3 { get; set; }
        public RecipeSlotItem B1 { get; set; }
        public RecipeSlotItem B2 { get; set; }
        public RecipeSlotItem B3 { get; set; }
        public RecipeSlotItem C1 { get; set; }
        public RecipeSlotItem C2 { get; set; }
        public RecipeSlotItem C3 { get; set; }

        public List<RecipeSlotItem> Items { get; set; }
    }
}
