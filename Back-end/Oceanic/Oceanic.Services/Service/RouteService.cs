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
        public IEnumerable<RoutesViewModel> GetRoutes(TransportTypeEnum transportType)
        {
            var requestHandler = new HttpWebRequestHandler();

            try
            {
                switch (transportType)
                {
                    case TransportTypeEnum.SEA:
                        return requestHandler.GetReleases(
                            "https://wa-eitvn.azurewebsites.net/index.php?r=api/routes").Result;
                    case TransportTypeEnum.CAR:
                        return requestHandler.GetReleases(
                            "https://wa-tlvn.azurewebsites.net/api/public/configuredRoutes").Result;
                    default:
                        throw new ArgumentException("transport type " + transportType + " not supported");
                }
            }
            catch (Exception e)
            {
                return new List<RoutesViewModel>();
            }
        }

        public IList<CalculatePrice> CalculatePriceExternal(IList<CalculatePriceViewModel> calculatePriceViewModel, 
            TransportTypeEnum transportType)
        {
            var requestHandler = new HttpWebRequestHandler();

            try
            {
                switch (transportType)
                {
                    case TransportTypeEnum.SEA:
                        return requestHandler.PostMethod(
                            "https://wa-eitvn.azurewebsites.net/index.php?r=api/price",
                            calculatePriceViewModel).Result;
                    case TransportTypeEnum.CAR:
                        return requestHandler.PostMethod(
                            "https://wa-tlvn.azurewebsites.net/api/public/caculatePrices",
                            calculatePriceViewModel).Result;
                    default:
                        throw new ArgumentException("transport type not supported");
                }
            }
            catch (Exception e)
            {
                return new List<CalculatePrice>();
            }
            
        }
    }
}
