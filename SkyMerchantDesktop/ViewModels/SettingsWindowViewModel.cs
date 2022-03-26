using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.Input;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Setting;
using SkyMerchantDesktop.Utils;

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

        public ICommand SaveCommand { get; set; }

        public SettingsWindowViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;
            //clone the settings, dont want updates to immediately be applied to app settings
            this.Settings = CloneUtils.DeepClone(App.settings);
            this.SaveCommand = new RelayCommand(async () => await SaveSettings());
        }

        private async Task SaveSettings()
        {
            await _settingsService.SaveSettings(Settings!);
            //dont want the App settings to be updated by any updates to this page made without saving.
            App.settings = CloneUtils.DeepClone(this.Settings);
            MessageBox.Show("Saved Settings!");
        }
    }
}
