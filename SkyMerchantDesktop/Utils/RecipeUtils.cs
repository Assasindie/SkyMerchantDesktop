using System;
using System.Text.RegularExpressions;

namespace SkyMerchantDesktop.Utils
{
    public class RecipeUtils
    {
        //convert "GIANT_FRAGMENT_BOULDER:1" to "GIANT_FRAGMENT_BOULDER", 1
        public static Tuple<string,int> GetQuantityNameFromRecipe(string item)
        {
            //match the number part
            string regex = Regex.Match(item, ":[0-9]*").Value;
            //remove number part from name
            string name = item.Replace(regex, "");
            //remove colon and parse to int
            int total = int.Parse(regex.Replace(":",""));
            return new Tuple<string, int>(name, total);
        }
    }
}