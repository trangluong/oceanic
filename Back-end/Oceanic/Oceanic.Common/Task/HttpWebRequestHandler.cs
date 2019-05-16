using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oceanic.Common.Model;

namespace Oceanic.Common
{
    public class HttpWebRequestHandler
    {
        public IEnumerable<RoutesViewModel> GetReleases(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                var myclass = JsonConvert.DeserializeObject<List<RoutesViewModel>>(response);
                return myclass;
            }
        }

        public async Task<List<CalculatePrice>> PostMethod(string url, IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(new Uri(url), calculatePriceViewModel).Result;
                return await response.Content.ReadAsAsync<List<CalculatePrice>>();
            }
        }

    }
}
