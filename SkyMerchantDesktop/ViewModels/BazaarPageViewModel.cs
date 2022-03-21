using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;

namespace SkyMerchantDesktop.ViewModels
{
    public class BazaarPageViewModel : BaseViewModel
    {
        private CollectionViewSource _recipeCostView;
        public CollectionViewSource RecipeCostView
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
        private IAuctionAPIService _auctionApiService;
        private IBazaarAPIService _bazaarApiService;
        private IRecipeAPIService _recipeApiService;
        private IRecipeService _recipeService;

        private async Task Initialise()  
        {
            _recipes = await _recipeApiService.GetLatestRecipes();
            //need to be created on the same thread. When updating doesnt matter about thread.
            await App.Current.Dispatcher.BeginInvoke(async () =>
           {
               await LoadLatestData();
               RecipeCostView = new CollectionViewSource()
               {
                   Source = RecipeCosts
               };

               RecipeCostView.IsLiveSortingRequested = true;
               RecipeCostView.SortDescriptions.Add(new SortDescription("difference", ListSortDirection.Descending));
           });
            //start timer that will regularly fetch new data
            timer = new System.Timers.Timer(120000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
         }

        public BazaarPageViewModel(IAuctionAPIService auctionApiService, IBazaarAPIService bazaarApiService,
            IRecipeAPIService recipeApiService, IRecipeService recipeService)
        {
            this._auctionApiService = auctionApiService;
            this._bazaarApiService = bazaarApiService;
            this._recipeApiService = recipeApiService;
            this._recipeService = recipeService;
            Task.Run(async () => await Initialise());
        }
       
        private async Task LoadLatestData()
        {
            //this will take some time to load lmoa
            _bazaars = await _bazaarApiService.GetAllBazaarItems();
            _auctions = await _auctionApiService.GetAllBINAuctions();
            RecipeCosts = new EnhancedObservableCollection<RecipeItem>(_recipeService.GetRecipeListWithCosts(_recipes, _auctions, _bazaars));
        }

        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await LoadLatestData();
        }

    }
}