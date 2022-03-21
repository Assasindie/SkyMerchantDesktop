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

        private RecipeSlotItem _A2;
        public RecipeSlotItem A2
        {
            get => _A2;
            set
            {
                _A2 = value;
                OnPropertyChanged();
            }
        }

        public RecipeSlotItem A3 { get; set; }
        public RecipeSlotItem B1 { get; set; }
        public RecipeSlotItem B2 { get; set; }
        public RecipeSlotItem B3 { get; set; }
        public RecipeSlotItem C1 { get; set; }
        public RecipeSlotItem C2 { get; set; }
        public RecipeSlotItem C3 { get; set; }
    }
}
