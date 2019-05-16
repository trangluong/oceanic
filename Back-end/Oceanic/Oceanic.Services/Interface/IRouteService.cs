using System.Collections.Generic;
using Oceanic.Common.Model;

namespace Oceanic.Services.Interface
{
    public interface IRouteService
    {
        IEnumerable<RoutesViewModel> GetRoutes(string transportType);

    }
}
