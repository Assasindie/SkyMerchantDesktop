using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Services
{
    public class APIRequestService
    {
        public readonly HttpClient client;
        private static string baseSkyblockUrl = $"https://api.hypixel.net/skyblock/";
        private static string baseSkyMerchantUrl = $"https://skymerchantapi.herokuapp.com/api/";
        private static string skyblockApiKey = "919ccdd2-9c60-49fb-b2ff-39b9a94ff52d"; //dont steal pls
        public APIRequestService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<HttpResponseMessage> MakeRequest(string route, string WebMethod, string data = "")
        {
            HttpResponseMessage res;
            StringContent content = null;

            if (data != null)
                content = new StringContent(data, Encoding.UTF8, "application/json");
            try
            {
                res = WebMethod switch
                {
                    "POST" => await client.PostAsync(route, content),
                    "PUT" => await client.PutAsync(route, content),
                    "PATCH" => await client.PatchAsync(route, content),
                    "DELETE" => await client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, route)
                    { Content = content }),
                    _ => await client.GetAsync(route) // default
                };
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                // remove all set headers so as not to transmit them to other services
                client.DefaultRequestHeaders.Authorization = null;
                client.DefaultRequestHeaders.Remove("API-Key");
            }
        }

        public async Task<T> MakeSkyblockRequest<T>(string route, string WebMethod, string data = "")
        {
            client.DefaultRequestHeaders.Add("API-Key", skyblockApiKey);
            //set route
            route = baseSkyblockUrl + route;
            HttpResponseMessage response = await MakeRequest(route, WebMethod, data);
            return GetFromStream<T>(await response.Content.ReadAsStreamAsync());
        }

        public async Task<T> MakeSkyMerchantRequest<T>(string route, string WebMethod, string data = "")
        {
            route = baseSkyMerchantUrl + route;
            HttpResponseMessage response = await MakeRequest(route, WebMethod, data);
            return GetFromStream<T>(await response.Content.ReadAsStreamAsync());
        }
        
        public async Task<T> MakeGenericRequest<T>(string route, string WebMethod, string data = "")
        {
            HttpResponseMessage response = await MakeRequest(route, WebMethod, data);
            return GetFromStream<T>(await response.Content.ReadAsStreamAsync());
        }

        private T GetFromStream<T>(Stream res)
        {
            using StreamReader sr = new(res);
            using JsonReader reader = new JsonTextReader(sr);
            JsonSerializer serializer = new()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            T response = serializer.Deserialize<T>(reader);
            return response;
        }
    }

}
