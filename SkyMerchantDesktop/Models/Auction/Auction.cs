using System;
using Newtonsoft.Json;

namespace SkyMerchantDesktop.Models
{
    public class Auction : IComparable<Auction>
    {
        public string id { get; set; }
        [JsonProperty("itemName")]
        public string itemName { get; set; }
        
        [JsonProperty("bid")]
        public decimal bid { get; set; }
        public bool bin { get; set; }
        public int count { get; set; }

        public string uuid { get; set; }
        public int CompareTo(Auction? obj)
        {
            if (this.bid > obj.bid)
            {
                return 1;
            }
            if (this.bid < obj.bid)
            {
                return -1;
            }
            return 0;
        }
        
    }
}