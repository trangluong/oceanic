using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Oceanic.Common.Model;

namespace Oceanic.Common
{
    public class HttpWebRequestHandler
    {

        public static readonly int HttpTimeoutSecs = 10;
        
        public async Task<IEnumerable<RoutesViewModel>> GetReleases(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(HttpTimeoutSecs);
                
                var response = await httpClient.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<List<RoutesViewModel>>();
            }
        }

        public async Task<List<CalculatePrice>> PostMethod(string url, IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(HttpTimeoutSecs);
                
                var response = await client.PostAsJsonAsync(new Uri(url), calculatePriceViewModel);

                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsAsync<List<CalculatePrice>>();
            }
        }

    }
}
