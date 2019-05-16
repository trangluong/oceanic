using System;
using System.Collections.Generic;
using Oceanic.Common;
using Oceanic.Common.Enum;
using Oceanic.Common.Model;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class RouteService : IRouteService
    {
        
        public RouteService() {}
        
        public IEnumerable<RoutesViewModel> GetRoutes(TransportTypeEnum transportType)
        {
            HttpWebRequestHandler task = new HttpWebRequestHandler();
             
            if (transportType == TransportTypeEnum.SEA)
            {
                return new List<RoutesViewModel>();
//                return task.GetReleases("https://wa-eitvn.azurewebsites.net/index.php?r=api/routes");
            }

            if (transportType == TransportTypeEnum.CAR)
            {
                return task.GetReleases("https://wa-tlvn.azurewebsites.net/api/public/configuredRoutes");
            }
            
            throw new ArgumentException("transport type not supported");
        }

        public IList<CalculatePrice> CalculatePriceExternal(IList<CalculatePriceViewModel> calculatePriceViewModel, 
            TransportTypeEnum transportType)
        {
            HttpWebRequestHandler task = new HttpWebRequestHandler();

            if (transportType == TransportTypeEnum.SEA)
            {
                return task.PostMethod("https://wa-eitvn.azurewebsites.net/index.php?r=api/price", 
                    calculatePriceViewModel).Result;
            }

            if (transportType == TransportTypeEnum.CAR)
            {
                return task.PostMethod("https://wa-tlvn.azurewebsites.net/api/public/caculatePrices", 
                    calculatePriceViewModel).Result;
            }
            
            throw new ArgumentException("transport type not supported");
        }
    }
}
