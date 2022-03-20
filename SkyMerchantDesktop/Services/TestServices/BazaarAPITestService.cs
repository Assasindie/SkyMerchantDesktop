using SkyMerchantDesktop.Models.Interfaces;
using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktopTests.Services
{
    public class BazaarAPITestService : IBazaarAPIService
    {
        public async Task<List<Bazaar>> GetAllBazaarItems()
        {
            return new List<Bazaar>()
            {
                new Bazaar()
                {
                    product_id = "Bazaar1",
                    buy_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        }
                    },
                    sell_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        }
                    }
                },
                new Bazaar()
                {
                   product_id = "Bazaar2",
                   buy_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        }
                    },
                    sell_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        }
                    }
                },
                new Bazaar()
                {
                    product_id = "Bazaar3",
                    buy_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 1,
                            pricePerUnit = new decimal(12.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 10,
                            pricePerUnit = new decimal(15.1),
                            orders = 1
                        }
                    },
                    sell_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1,
                            pricePerUnit = new decimal(10.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 1,
                            pricePerUnit = new decimal(12.1),
                            orders = 1
                        },
                        new BazaarCostSummary()
                        {
                            amount = 10,
                            pricePerUnit = new decimal(15.1),
                            orders = 1
                        }
                    }
                },
                new Bazaar()
                {
                    product_id = "Revenant_Viscera",
                    buy_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10),
                            orders = 1
                        }
                    },
                    sell_summary = new BazaarCostSummary[]
                    {
                        new BazaarCostSummary()
                        {
                            amount = 1234,
                            pricePerUnit = new decimal(10),
                            orders = 1
                        }
                    }
                },
            };
        }
    }
}
