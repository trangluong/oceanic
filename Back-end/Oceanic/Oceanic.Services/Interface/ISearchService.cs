using System.Collections.Generic;
using Oceanic.Common.Model;
using Oceanic.Core;

namespace Oceanic.Services.Interface
{
    public interface ISearchService 
    {
        IEnumerable<City> LoadCity();

        List<RouteSearchViewModel> SearchRoutes(RouteSearchRequest sr);
    }
}
