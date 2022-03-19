using SkyMerchantDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SkyMerchantDesktop;

namespace SkyMerchantDesktopTests
{
    public class BaseUnitTest
    {
        [OneTimeSetUp]
        protected virtual void OneTimeSetup()
        {
            App.APIRequestService = new APIRequestService();
        }
    }
}
