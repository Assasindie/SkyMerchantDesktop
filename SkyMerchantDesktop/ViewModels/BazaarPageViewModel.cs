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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using SkyMerchantDesktop.Models;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Recipe;
using SkyMerchantDesktop.Models.Setting;
using SkyMerchantDesktop.Services;
using SkyMerchantDesktop.Utils;
using SkyMerchantDesktop.Views;

namespace SkyMerchantDesktop.ViewModels
{
    public class BazaarPageViewModel : BaseViewModel
    {
        private CollectionViewSource _recipeCostView;
        public CollectionViewSource RecipeCostView
        {
            get { return _recipeCostView; }
            set { _recipeCostView = value; OnPropertyChanged(); }
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

        private EnhancedObservableCollection<Auction> _userAuctions;

        public EnhancedObservableCollection<Auction> userAuctions
        {
            get => _userAuctions;
            set
            {
                _userAuctions = value;
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
        private IAuctionService _auctionService;

        private RecipeItem _selectedItem;
        public RecipeItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                GetSelectedItemVisualRecipe(value);
                OnPropertyChanged();
            }
        }

        private VisualRecipe _selectedItemRecipe;
        public VisualRecipe SelectedItemRecipe
        {
            get => _selectedItemRecipe;
            set
            {
                _selectedItemRecipe = value;
                OnPropertyChanged();
            }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
                //trigger the filter event when 
                RecipeCostView.Filter += RecipeCostView_Filter;

            }
        }

        public ICommand SettingsWindowCommand { get; set; }


        private async Task Initialise()
        {
            _recipes = await _recipeApiService.GetLatestRecipes();
            //need to be created on the same thread. When updating doesnt matter about thread.
            await LoadLatestData();
            await App.Current.Dispatcher.BeginInvoke(async () =>
            {
                RecipeCostView = new CollectionViewSource()
                {
                    Source = RecipeCosts
                };

                RecipeCostView.IsLiveSortingRequested = true;
                RecipeCostView.SortDescriptions.Add(new SortDescription("difference", ListSortDirection.Descending));
                RecipeCostView.IsLiveFilteringRequested = true;
            });

            SelectedItemRecipe = new VisualRecipe
            {
                Items = new()
            };
            //start timer that will regularly fetch new data
            timer = new System.Timers.Timer(120000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void RecipeCostView_Filter(object sender, FilterEventArgs e)
        {
            RecipeItem item = (RecipeItem)e.Item;
            if (string.IsNullOrEmpty(SearchQuery))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = item.name.ToLower().Contains(SearchQuery);
            }
        }

        public BazaarPageViewModel(IAuctionAPIService auctionApiService, IBazaarAPIService bazaarApiService,
            IRecipeAPIService recipeApiService, IRecipeService recipeService, IAuctionService auctionService)
        {
            this._auctionApiService = auctionApiService;
            this._bazaarApiService = bazaarApiService;
            this._recipeApiService = recipeApiService;
            this._recipeService = recipeService;
            this._auctionService = auctionService;
            Task.Run(async () => await Initialise());
            SettingsWindowCommand = new RelayCommand(async () => await LoadSettingsWindow());
        }

        private async Task LoadLatestData()
        {
            //this will take some time to load lmoa
            _bazaars = await _bazaarApiService.GetAllBazaarItems();
            _auctions = await _auctionApiService.GetAllBINAuctions();
            List<RecipeItem> items = _recipeService.GetRecipeListWithCosts(_recipes, _auctions, _bazaars);
            await App.Current.Dispatcher.BeginInvoke(async () =>
            {
                if (RecipeCosts == null) RecipeCosts = new EnhancedObservableCollection<RecipeItem>();
                RecipeCosts.ClearList();
                foreach (RecipeItem item in items)
                {
                    RecipeCosts.Add(item);
                }

                List<Auction> auctions = new(_auctionService.FilterAuctionsByUser(App.settings.uuid, _auctions));
                if (userAuctions == null) userAuctions = new EnhancedObservableCollection<Auction>();
                userAuctions.ClearList();
                foreach (Auction auction in auctions)
                {
                    userAuctions.Add(auction);
                }
            });

        }

        private async void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await LoadLatestData();
        }

        private void GetSelectedItemVisualRecipe(RecipeItem recipe)
        {
            SelectedItemRecipe = RecipeUtils.TransformRecipeToVisualRecipe(recipe.recipe, _recipeService, _bazaars, _auctions);
        }

        private async Task LoadSettingsWindow()
        {

            await App.Current.Dispatcher.BeginInvoke(async () =>
            {
                SettingsWindow window = App.ServiceProvider.GetRequiredService<SettingsWindow>();
                window.Show();
            });

        }
    }
}