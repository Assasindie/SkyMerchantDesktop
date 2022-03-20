using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static APIRequestService APIRequestService;
        public static IAuctionAPIService AuctionApiService;
        public static IBazaarAPIService BazaarApiService;
        public static IRecipeAPIService RecipeApiService;
        public App()
        {
            APIRequestService = new APIRequestService();
            AuctionApiService = new AuctionApiTestService();
            BazaarApiService = new BazaarAPITestService();
            RecipeApiService = new RecipeAPITestService();
            InitializeComponent();
        }
    }
}
