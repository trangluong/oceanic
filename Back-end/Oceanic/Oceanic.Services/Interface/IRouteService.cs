using System.Collections.Generic;
using Oceanic.Common.Enum;
using Oceanic.Common.Model;

namespace Oceanic.Services.Interface
{
    public interface IRouteService
    {
        IEnumerable<RoutesViewModel> GetRoutes(TransportTypeEnum transportType);

        IList<CalculatePrice> CalculatePriceExternal(IList<CalculatePriceViewModel> calculatePriceViewModel,
            TransportTypeEnum transportType);

    }
}
