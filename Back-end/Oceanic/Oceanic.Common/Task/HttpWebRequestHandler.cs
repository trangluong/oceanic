using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Services;
using System.Net;
using System.Net.Http;
using Oceanic;
using Oceanic.Common.Model;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;

namespace Oceanic.Common.Task
{
    public class HttpWebRequestHandler
    {
        public IEnumerable<RoutesViewModel> GetReleases(string url)
        {
            using (var httpClient = new HttpClient())
            {
        

                var response = httpClient.GetStringAsync(new Uri(url)).Result;
                var myclass = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoutesViewModel>>(response);
                return myclass;
            }
        }

        public IList<CalculatePrice>PostMethod(string url, IList<CalculatePriceViewModel> calculatePriceViewModel)
        {
            IList<CalculatePrice> result = new List<CalculatePrice>();
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(calculatePriceViewModel);
            using (var client = new HttpClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(new Uri(url), jsonBody).Result;
                var responseContent =  response.Content.ToString();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CalculatePrice>>(responseContent);
            }
            return result;
        }

    }
}
