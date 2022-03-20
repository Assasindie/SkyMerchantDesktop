namespace SkyMerchantDesktop.Models.Recipe
{
    public class RecipeItem : BaseObservableModel
    {
        private string? _name { get; set; }
        public string? name
        {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public Recipe? recipe { get; set; }
        public string? wiki { get; set; }

        private decimal _lowestAuction;
        public decimal lowestAuction
        {
            get => _lowestAuction;
            set
            {
                _lowestAuction = value;
                OnPropertyChanged(nameof(lowestAuction));
            }
        }

        private decimal _bazaarCost;
        public decimal bazaarCost {
            get => _bazaarCost;
            set
            {
                _bazaarCost = value;
                OnPropertyChanged(nameof(bazaarCost));
            }
        }

        private decimal _difference;
        public decimal difference
        {
            get => _difference;
            set
            {
                _difference = value;
                OnPropertyChanged(nameof(difference));
            }
        }
    }
}