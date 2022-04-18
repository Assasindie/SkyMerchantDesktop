using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Setting;
using SkyMerchantDesktop.ViewModels;
using SkyMerchantDesktop.Views;
using SkyMerchantDesktopTests.Services;

namespace SkyMerchantDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static APIRequestService APIRequestService;
        public static IServiceProvider ServiceProvider;
        public static Settings? settings;

        public App()
        {
            APIRequestService = new APIRequestService();
            IServiceCollection services = new ServiceCollection();
            RegisterServices(services);
            ServiceProvider = services.BuildServiceProvider();
            settings = ServiceProvider.GetRequiredService<ISettingsService>().LoadSettings().Result;
            //initialise an empty settings if the settings file does not exist
            if(settings == null)
            {
                settings = new Settings();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            BazaarPage page = ServiceProvider.GetRequiredService<BazaarPage>();
            page.Show();
        }

        private void RegisterServices(IServiceCollection services)
        {
            //depending on the current build config use different api service
            #if DEBUG
            services.AddSingleton<IAuctionAPIService, AuctionApiTestService>();
            services.AddSingleton<IBazaarAPIService, BazaarAPITestService>();
            services.AddSingleton<IRecipeAPIService, RecipeAPITestService>();
            services.AddTransient<IMojangAPIService, MojangAPITestService>();
            #else
            services.AddSingleton<IAuctionAPIService, AuctionApiService>();
            services.AddSingleton<IBazaarAPIService, BazaarAPIService>();
            services.AddSingleton<IRecipeAPIService, RecipeAPIService>();
            services.AddTransient<IMojangAPIService, MojangAPIService>();
            #endif

            services.AddSingleton<IRecipeService, RecipeService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddTransient<IAuctionService, AuctionService>();


            services.AddSingleton<BazaarPageViewModel>();
            services.AddSingleton<BazaarPage>(s => new BazaarPage()
            {
                DataContext = s.GetRequiredService<BazaarPageViewModel>()
            });

            services.AddTransient<SettingsWindowViewModel>();
            services.AddTransient<SettingsWindow>(s => new SettingsWindow()
            {
                DataContext = s.GetRequiredService<SettingsWindowViewModel>()
            });
        }
    }
}
