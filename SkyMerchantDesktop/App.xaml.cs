using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SkyMerchantDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static APIRequestService APIRequestService;
        public App()
        {
            InitializeComponent();
            APIRequestService = new APIRequestService();
        }
    }
}
