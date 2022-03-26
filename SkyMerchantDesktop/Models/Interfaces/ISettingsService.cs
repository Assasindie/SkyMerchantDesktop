using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyMerchantDesktop.Models.Setting;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface ISettingsService
    {
        public Task<Settings?> LoadSettings();
        public Task SaveSettings(Settings settings);
    }
}
