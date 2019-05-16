using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Oceanic.Common.Model;

namespace Oceanic.Common.Task
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

        public IList<CalculatePrice>PostMethod(string url, IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            IList<CalculatePrice> result = new List<CalculatePrice>();
            var jsonBody = JsonConvert.SerializeObject(calculatePriceViewModel);
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(new Uri(url), jsonBody).Result;
                var responseContent =  response.Content.ToString();
                result = JsonConvert.DeserializeObject<List<CalculatePrice>>(responseContent);
            }
            return result;
        }

    }
}
