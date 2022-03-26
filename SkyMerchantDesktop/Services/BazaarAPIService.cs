using Newtonsoft.Json;
using SkyMerchantDesktop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Services
{
    public class BazaarAPIService : IBazaarAPIService
    {

        public BazaarAPIService()
        {

        }

        public async Task<List<Bazaar>> GetAllBazaarItems()
        {
            //take response which is a bunch of objects with static names and turn into a list so can display and easily query.
            BazaarResponse? request = await App.APIRequestService.MakeSkyblockRequest<BazaarResponse?>("bazaar", "GET");
            if (request == null) return new List<Bazaar>();
            BazaarProductResponse response = request.products;
            PropertyInfo[] properties = typeof(BazaarProductResponse).GetProperties();
            List<Bazaar> bazaarItems = new();
            foreach (PropertyInfo info in properties)
            {
                //this foreach goes through every single variable in BazaarProductResponse which will be all the bazaaritems
                Bazaar baz = new Bazaar();
                PropertyInfo[] itemProperties = info.GetValue(response)!.GetType().GetProperties();
                foreach (PropertyInfo property in itemProperties)
                {
                    //this foreach will go thru all the variables within a Bazaar product and assign them to the baz product
                    switch (property.Name)
                    {
                        case "product_id":
                            baz.product_id = (string?) property.GetValue(info.GetValue(response));
                            break;
                        case "sell_summary":
                            baz.sell_summary = (BazaarCostSummary[]?) property.GetValue(info.GetValue(response));

                            break;
                        case "buy_summary":
                            baz.buy_summary = (BazaarCostSummary[]?) property.GetValue(info.GetValue(response));

                            break;
                        default: break;
                    }
                }
                //order so lowest cost first
                baz.sell_summary = baz.sell_summary?.OrderBy(o => o.pricePerUnit).ToArray();
                baz.buy_summary = baz.buy_summary?.OrderBy(o => o.pricePerUnit).ToArray();
                bazaarItems.Add(baz);
            }
            return bazaarItems;
        }
    }
}
