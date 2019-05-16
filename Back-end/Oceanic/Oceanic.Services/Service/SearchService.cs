using System;
using System.Collections.Generic;
using System.Linq;
using Oceanic.Common.Model;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;
using QuickGraph;
using QuickGraph.Algorithms;

namespace Oceanic.Services.Service
{
    public class SearchService : ISearchService
    {
        private readonly IRepositoryAsync<City> _cityRepository;
        private readonly IRepositoryAsync<Route> _routeRepository;
        private readonly IRepositoryAsync<TransportType> _transportTypeRepository;
        private readonly IRepositoryAsync<GoodsType> _goodsTypeRepository;
        private readonly IAdminService _adminService;
        private readonly IRouteService _routeService;

        public SearchService(IRepositoryAsync<City> cityRepository, 
            IRepositoryAsync<Route> routeRepository,
            IRepositoryAsync<TransportType> transportTypeRepository,
            IAdminService adminService,
            IRepositoryAsync<GoodsType> goodsTypeRepository,
            IRouteService routeService) 
        {
            this._cityRepository = cityRepository;
            this._routeRepository = routeRepository;
            this._transportTypeRepository = transportTypeRepository;
            this._adminService = adminService;
            this._goodsTypeRepository = goodsTypeRepository;
            this._routeService = routeService;
        }
        public IEnumerable<City> LoadCity()
        {
            return _cityRepository.Query().Select().ToList();
        }

        private BidirectionalGraph<string, TaggedEdge<string, string>> buildGraph(List<RouteSearchModel> routes)
        {
            var graph = new BidirectionalGraph<string, TaggedEdge<string, string>>();
            
            routes.ForEach(route =>
            {
                graph.AddVertex(route.from_city);
                graph.AddVertex(route.to_city);
                graph.AddEdge(new TaggedEdge<string, string>(route.from_city, route.to_city, route.transportType));
            });

            return graph;
        }

        private List<RouteSearchModel> GetAllRoutes(Dictionary<int, City> cityDict)
        {
            var airplaneRoutes = _routeRepository.Query(r => r.IsActive).Select().ToList();
            var airplaneRouteModels = new List<RouteSearchModel>();
            foreach (var r in airplaneRoutes)
            {
                airplaneRouteModels.Add(new RouteSearchModel
                {
                    from_city = cityDict[r.FromCityId].Code,
                    to_city = cityDict[r.ToCityId].Code,
                    hours = r.LongHour,
                    segment = r.Segments,
                    transportType = "Airplane"
                });
            }

            List<RouteSearchModel> ToModels(IEnumerable<RoutesViewModel> ms, string transportType)
            {
                var res = new List<RouteSearchModel>();
                foreach (var m in ms)
                {
                    res.Add(new RouteSearchModel
                    {
                        from_city = m.from_city,
                        to_city = m.to_city,
                        hours = m.hours,
                        segment = m.segment,
                        transportType = transportType
                    });
                }
                return res;
            }
            
            var seaRouteModels = ToModels(_routeService.GetRoutes("Sea"), "Sea");
            
            var carRouteModels = ToModels(_routeService.GetRoutes("Car"), "Car");
            
            var otherRoutes = seaRouteModels.Concat(carRouteModels);

            return airplaneRouteModels.Concat(otherRoutes).ToList();
        }

        public List<RouteSearchViewModel> SearchRoutes(RouteSearchRequest sr)
        {
            var cities = _cityRepository.Query().Select().ToList();
            var cityById = cities.ToDictionary(c => c.Id);
            
            var routes = GetAllRoutes(cityById);

            var routeDict = routes.ToDictionary(r => (r.from_city, r.to_city, r.transportType));

            var gtDict = _goodsTypeRepository.Query().Select().ToDictionary(g => g.Id);

            var graph = buildGraph(routes);

            var cpr = new List<CalculatePriceViewModel>
            {
                new CalculatePriceViewModel
                {
                    weight = sr.weight,
                    height = sr.height,
                    width = sr.breadth,
                    length = sr.depth,
                    goods_type = gtDict[sr.goodsTypeId].Code
                }
            };
            var pricePerSegment = _adminService.CalculatePrices(cpr).First();

            double WeightByPrice(TaggedEdge<string, string> edge)
            {
                var key = (edge.Source, edge.Target, edge.Tag);
                var basePrice = (double) (pricePerSegment.price * routeDict[key].segment);
                switch (routeDict[key].transportType)
                {
                    case "Airplane":
                        return basePrice;
                    case "Sea":
                        return basePrice * (double) sr.weight;
                    case "Car":
                        return basePrice;
                    default:
                        return basePrice;
                }
            }
            
            double WeightByTime(TaggedEdge<string, string> edge)
            {
                var key = (edge.Source, edge.Target, edge.Tag);
                return routeDict[key].hours;
            }

            IEnumerable<IEnumerable<TaggedEdge<string, string>>> ShortestPaths(Func<TaggedEdge<string, string>, double> weightFunc)
            {
                try
                {
                    return graph.RankedShortestPathHoffmanPavley(
                        weightFunc, cityById[sr.fromCityId].Code, cityById[sr.toCityId].Code, 2);
                }
                catch (KeyNotFoundException e)
                {
                    return new List<List<TaggedEdge<string, string>>>();
                }
            }

            var shortestPaths = ShortestPaths(WeightByTime).Concat(ShortestPaths(WeightByPrice));

            var result = new List<RouteSearchViewModel>();

            var transportTypeByCode = _transportTypeRepository.Query().Select().ToDictionary(t => t.Code);

            var cityByCode = cities.ToDictionary(c => c.Code);
            
            foreach (var shortestPath in shortestPaths)
            {
                var routeModel = new RouteSearchViewModel();
                var parts = new List<RouteViewModel>();
                var estimatedTime = 0;
                var price = 0.0;
                
                foreach (var edge in shortestPath)
                {
                    var key = (edge.Source, edge.Target, edge.Tag);
                    parts.Add(new RouteViewModel
                    {
                        fromCity = cityByCode[edge.Source].Name,
                        toCity = cityByCode[edge.Target].Name,
                        transportType = transportTypeByCode[routeDict[key].transportType].Name
                    });

                    estimatedTime += Convert.ToInt32(WeightByTime(edge));
                    price += WeightByPrice(edge);
                }

                routeModel.parts = parts;
                routeModel.estimatedTime = estimatedTime;
                routeModel.price = price;
                
                result.Add(routeModel);
            }

            return result;
        }
    }
}
