using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Models.Interfaces
{
    public interface IBazaarAPIService
    {
        public Task<List<Bazaar>> GetAllBazaarItems();
    }
}
