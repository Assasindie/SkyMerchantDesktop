using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Models.Setting;

namespace SkyMerchantDesktop.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly string _settingsPath = Environment.CurrentDirectory + @"\settings.json";
        public async Task<Settings?> LoadSettings()
        {
            if (!File.Exists(_settingsPath)) return null;
            string json = await File.ReadAllTextAsync(_settingsPath);
            return JsonConvert.DeserializeObject<Settings>(json);
        }

        public async Task SaveSettings(Settings? settings)
        {
            if (settings == null) return;
            string json = JsonConvert.SerializeObject(settings);
            await using (StreamWriter sw = File.CreateText(_settingsPath))
            {
                await sw.WriteAsync(json);
            }
        }
    }
}