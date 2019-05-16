using System.Collections.Generic;
using Oceanic.Core;

namespace Oceanic.Services.Service
{
    public class RouteComparer : IEqualityComparer<Route>
    {
        public bool Equals(Route x, Route y)
        {
            return (x.FromCityId, x.ToCityId).Equals((y.FromCityId, y.ToCityId));
        }

        public int GetHashCode(Route obj)
        {
            return (obj.FromCityId, obj.ToCityId).GetHashCode();
        }
    }
}