using System;
using System.Collections.Generic;
using System.Linq;
using Oceanic.Common.Enum;
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

        private BidirectionalGraph<string, TaggedEdge<string, TransportTypeEnum>> buildGraph(List<RouteSearchModel> routes)
        {
            var graph = new BidirectionalGraph<string, TaggedEdge<string, TransportTypeEnum>>();
            
            routes.ForEach(route =>
            {
                graph.AddVertex(route.from_city);
                graph.AddVertex(route.to_city);
                graph.AddEdge(new TaggedEdge<string, TransportTypeEnum>(route.from_city, route.to_city, route.transportType));
            });

            return graph;
        }

        private List<RouteSearchModel> GetAllRoutes(
            HashSet<TransportTypeEnum> transportTypes, Dictionary<int, City> cityDict)
        {
            var airplaneRouteModels = new List<RouteSearchModel>();
            if (transportTypes.Contains(TransportTypeEnum.AIRPLANE))
            {
                var airplaneRoutes = _routeRepository.Query(r => r.IsActive).Select().ToList();
                foreach (var r in airplaneRoutes)
                {
                    airplaneRouteModels.Add(new RouteSearchModel
                    {
                        from_city = cityDict[r.FromCityId].Code,
                        to_city = cityDict[r.ToCityId].Code,
                        hours = r.LongHour,
                        segment = r.Segments,
                        transportType = TransportTypeEnum.AIRPLANE
                    });
                }
            }
            
            List<RouteSearchModel> ToModels(
                IEnumerable<RoutesViewModel> ms, TransportTypeEnum transportType)
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

            var seaRouteModels = new List<RouteSearchModel>();
            if (transportTypes.Contains(TransportTypeEnum.SEA))
            {
                seaRouteModels = ToModels(_routeService.GetRoutes(TransportTypeEnum.SEA), TransportTypeEnum.SEA);
            }

            var carRouteModels = new List<RouteSearchModel>();
            if (transportTypes.Contains(TransportTypeEnum.CAR))
            {
                carRouteModels = ToModels(_routeService.GetRoutes(TransportTypeEnum.CAR), TransportTypeEnum.CAR);
            }
            
            var otherRoutes = seaRouteModels.Concat(carRouteModels);
            return airplaneRouteModels.Concat(otherRoutes).ToList();
        }

        private Dictionary<TransportTypeEnum, CalculatePrice> GetAllPrices(RouteSearchRequest sr)
        {
            var gtDict = _goodsTypeRepository.Query().Select().ToDictionary(g => g.Id);
            
            var cpm = new List<CalculatePriceViewModel>
            {
                new CalculatePriceViewModel
                {
                    weight = Convert.ToInt32(sr.weight),
                    height = sr.height,
                    width = sr.breadth,
                    length = sr.depth,
                    goods_type = gtDict[sr.goodsTypeId].Code
                }
            };

            var res = new Dictionary<TransportTypeEnum, CalculatePrice>();

            void AddPrice(TransportTypeEnum tt, CalculatePrice p)
            {
                if (p.status > 0)
                {
                    res.Add(tt, p);
                }
            }
            
            var airplanePrice = _adminService.CalculatePrices(cpm).First();
            AddPrice(TransportTypeEnum.AIRPLANE, airplanePrice);

            var seaPrice = _routeService.CalculatePriceExternal(cpm, TransportTypeEnum.SEA).First();
            AddPrice(TransportTypeEnum.SEA, seaPrice);
            
            var carPrice = _routeService.CalculatePriceExternal(cpm, TransportTypeEnum.CAR).First();
            AddPrice(TransportTypeEnum.CAR, carPrice);
            
            return res;
        }

        public List<RouteSearchViewModel> SearchRoutes(RouteSearchRequest sr)
        {
            var basePrices = GetAllPrices(sr);
            
            var cities = _cityRepository.Query().Select().ToList();
            var cityById = cities.ToDictionary(c => c.Id);
            
            var routes = GetAllRoutes(basePrices.Keys.ToHashSet(), cityById);

            var routeDict = routes.ToDictionary(r => (r.from_city, r.to_city, r.transportType));

            var graph = buildGraph(routes);

            double WeightByPrice(TaggedEdge<string, TransportTypeEnum> edge)
            {
                var key = (edge.Source, edge.Target, edge.Tag);
                switch (routeDict[key].transportType)
                {
                    case TransportTypeEnum.AIRPLANE:
                        return (double) (basePrices[TransportTypeEnum.AIRPLANE].price);
                    case TransportTypeEnum.CAR:
                        return (double) (basePrices[TransportTypeEnum.AIRPLANE].price * routeDict[key].segment);
                    case TransportTypeEnum.SEA:
                        return (double) (basePrices[TransportTypeEnum.SEA].price * sr.weight * routeDict[key].segment);
                    default:
                        throw new ArgumentException("transport type not supported");
                }
            }
            
            double WeightByTime(TaggedEdge<string, TransportTypeEnum> edge)
            {
                var key = (edge.Source, edge.Target, edge.Tag);
                return routeDict[key].hours;
            }

            IEnumerable<IEnumerable<TaggedEdge<string, TransportTypeEnum>>> ShortestPaths(
                Func<TaggedEdge<string, TransportTypeEnum>, double> weightFunc)
            {
                try
                {
                    return graph.RankedShortestPathHoffmanPavley(
                        weightFunc, cityById[sr.fromCityId].Code, cityById[sr.toCityId].Code, 2);
                }
                catch (KeyNotFoundException e)
                {
                    return new List<List<TaggedEdge<string, TransportTypeEnum>>>();
                }
            }

            var shortestPaths = ShortestPaths(WeightByTime).Concat(ShortestPaths(WeightByPrice));

            var result = new List<RouteSearchViewModel>();

            var transportTypeByCode = _transportTypeRepository.Query().Select().ToDictionary(t =>
            {
                switch (t.Code)
                {
                    case "Airplane":
                        return TransportTypeEnum.AIRPLANE;
                    case "Sea":
                        return TransportTypeEnum.SEA;
                    case "Car":
                        return TransportTypeEnum.CAR;
                    default:
                        return TransportTypeEnum.AIRPLANE;
                }
            });

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
