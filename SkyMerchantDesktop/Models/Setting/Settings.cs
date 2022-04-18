using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyMerchantDesktop.ViewModels;

namespace SkyMerchantDesktop.Models.Setting
{
    public class Settings : BaseViewModel
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _skyblockAPIKey;

        public string SkyblockAPIKey
        {
            get => _skyblockAPIKey;
            set
            {
                _skyblockAPIKey = value;
                OnPropertyChanged();
            }
        }

        private string _uuid;

        public string uuid
        {
            get => _uuid;
            set
            {
                _uuid = value;
                OnPropertyChanged();
            }
        }
        
        public Settings()
        {
            
        }
    }
}
