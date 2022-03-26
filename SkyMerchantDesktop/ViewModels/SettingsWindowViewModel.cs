using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Setting;

namespace SkyMerchantDesktop.ViewModels
{
    public class SettingsWindowViewModel : BaseViewModel
    {
        private Settings? _settings;

        public Settings? Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged();
            }
        }

        private ISettingsService _settingsService;
        
        public SettingsWindowViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            this.Settings = App.settings;
        }

        private async Task LoadSettings()
        {
            this.Settings = await _settingsService.LoadSettings();
        }

        private async Task SaveSettings()
        {
            await _settingsService.SaveSettings(Settings!);
        }
    }
}
