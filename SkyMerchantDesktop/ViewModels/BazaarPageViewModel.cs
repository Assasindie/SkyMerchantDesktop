using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;

namespace SkyMerchantDesktop.ViewModels
{
    public class BazaarPageViewModel : BaseViewModel
    {
        private ListCollectionView _recipeCostView;
        public ListCollectionView RecipeCostView
        {
            get { return _recipeCostView; }
            set { _recipeCostView = value; OnPropertyChanged();}
        }

        private EnhancedObservableCollection<RecipeItem> _recipeCosts;
        public EnhancedObservableCollection<RecipeItem> RecipeCosts
        {
            get => _recipeCosts;
            set
            {
                _recipeCosts = value;
                OnPropertyChanged();
            }
        }
        private List<Bazaar> _bazaars;
        private List<Auction> _auctions;
        private List<RecipeItem> _recipes;
        private System.Timers.Timer timer;

        public ICommand SortByDifferenceCommand;

        private async Task Initialise()  
        {
            _recipes = await App.RecipeApiService.GetLatestRecipes();
            await LoadLatestData();
            RecipeCostView = new ListCollectionView(RecipeCosts);
            RecipeCostView.IsLiveSorting = true;
            RecipeCostView.SortDescriptions.Add(new SortDescription("lowestAuction", ListSortDirection.Descending));
            RecipeCostView.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Descending));

            //start timer that will regularly fetch new data
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
         }

        public BazaarPageViewModel()
        {
            Task.Run(() => Initialise());
            //this.SortByDifferenceCommand = new RelayCommand((x) => SortByDifference());
        }
       
        private async Task LoadLatestData()
        {
            //this will take some time to load lmoa
            _bazaars = await App.BazaarApiService.GetAllBazaarItems();
            _auctions = await App.AuctionApiService.GetAllBINAuctions();
            RecipeCosts = new EnhancedObservableCollection<RecipeItem>(new RecipeService().GetRecipeListWithCosts(_recipes, _auctions, _bazaars));
        }

        private void SortByDifference()
        {
            RecipeCosts[new Random().Next(1, 4)].difference = new Random().Next(-1000000, 1000000);
                       
           // RecipeCosts = new EnhancedObservableCollection<RecipeItem>(x);

        }


        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            //await LoadLatestData();
            SortByDifference();
        }

    }
}