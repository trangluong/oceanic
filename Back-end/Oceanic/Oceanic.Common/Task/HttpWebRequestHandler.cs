using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Oceanic.Common.Enum;
using Oceanic.Common.Model;
using RestSharp;

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

        public async Task<List<CalculatePrice>> PostMethod(string url,
            IList<CalculatePriceViewModel> calculatePriceViewModel, 
            TransportTypeEnum transportType)
        {
            if (transportType == TransportTypeEnum.CAR)
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
            else
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Connection", "keep-alive");
                request.AddHeader("content-length", "105");
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Host", "wa-eitvn.azurewebsites.net");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");


                var body = new List<CalculateSeaPriceViewModel>();
                foreach (var m in calculatePriceViewModel)
                {
                    body.Add(new CalculateSeaPriceViewModel
                    {
                        goods_type = m.goods_type,
                        height = m.height,
                        weight = m.weight,
                        length = m.length,
                        width = m.width,
                        departure_date = ""
                    });
                }

                var json = JsonConvert.SerializeObject(body);
                request.AddParameter("undefined", json, RestSharp.ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                return JsonConvert.DeserializeObject<List<CalculatePrice>>(response.Content);
            }
            
        }

    }
}
