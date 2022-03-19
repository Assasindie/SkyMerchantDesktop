using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Services
{
    public class Bazaar
    {
        public string? product_id { get; set; }
        public BazaarCostSummary[]? sell_summary { get; set; }
        public BazaarCostSummary[]? buy_summary { get; set; }
    }
}
