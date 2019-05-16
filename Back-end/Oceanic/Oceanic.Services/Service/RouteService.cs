using System;
using System.Collections.Generic;
using Oceanic.Common.Model;
using Oceanic.Common.Task;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class RouteService : IRouteService
    {
        
        public RouteService() {}
        
        public IEnumerable<RoutesViewModel> GetRoutes(string transportType)
        {
            HttpWebRequestHandler task = new HttpWebRequestHandler();
             
            if (transportType == "Sea")
            {
                return task.GetReleases("https://wa-eitvn.azurewebsites.net/index.php?r=api/routes");
            }

            if (transportType == "Car")
            {
                return task.GetReleases("https://wa-tlvn.azurewebsites.net/api/public/configuredRoutes");
            }
            
            throw new ArgumentException("transport type not supported");
        }
    }
}
