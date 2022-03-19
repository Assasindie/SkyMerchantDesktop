namespace SkyMerchantDesktop.Models.Recipe
{
    public class RecipeItem
    {
        public string? name { get; set; }
        public Recipe? recipe { get; set; }
        public string? wiki { get; set; }
        public decimal lowestAuction { get; set; }
        public decimal bazaarCost { get; set; }
    }
}