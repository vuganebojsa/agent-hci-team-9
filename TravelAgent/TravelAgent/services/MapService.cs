using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgent.services
{
    public class MapService
    {
        public static string BingMapsKey = "Ijvu2SMwlr7DxdnfTKKG~kJisNvX_vlWR5egGU2rIww~AmHWNbRje2l-vERM1_qq5tn-t3ZiEAY8C5KumLNp01RXxFPkFKOBHOpieJsW9C1T";


        public static async Task<string> ReverseGeocodeAsync(double latitude, double longitude)
        {
            string requestUrl = $"http://dev.virtualearth.net/REST/v1/Locations/{latitude},{longitude}?o=json&key={BingMapsKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    return data.resourceSets[0].resources[0].name;
                }
                else
                {
                    throw new Exception($"Reverse geocoding request failed with status code: {response.StatusCode}");
                }
            }
        }
    }
}
