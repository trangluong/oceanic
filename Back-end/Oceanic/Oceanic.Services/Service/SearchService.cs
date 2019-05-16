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

        public SearchService(IRepositoryAsync<City> cityRepository, 
            IRepositoryAsync<Route> routeRepository,
            IRepositoryAsync<TransportType> transportTypeRepository,
            IAdminService adminService,
            IRepositoryAsync<GoodsType> goodsTypeRepository) 
        {
            this._cityRepository = cityRepository;
            this._routeRepository = routeRepository;
            this._transportTypeRepository = transportTypeRepository;
            this._adminService = adminService;
            this._goodsTypeRepository = goodsTypeRepository;
        }
        public IEnumerable<City> LoadCity()
        {
            return _cityRepository.Query().Select().ToList();
        }

        private BidirectionalGraph<int, Edge<int>> buildGraph(List<Route> routes)
        {
            BidirectionalGraph<int, Edge<int>> graph = new BidirectionalGraph<int, Edge<int>>();
            
            routes.ForEach(route =>
            {
                graph.AddVertex(route.FromCityId);
                graph.AddVertex(route.ToCityId);
                graph.AddEdge(new Edge<int>(route.FromCityId, route.ToCityId));
            });

            return graph;
        }

        public List<RouteSearchViewModel> SearchRoutes(RouteSearchRequest sr)
        {
            var routes = _routeRepository.Query(r => r.IsActive).Select().ToList();
            var routeDict = routes.ToDictionary(r => (r.FromCityId, r.ToCityId));

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

            double EdgeWeights(Edge<int> edge)
            {
                return (double) (pricePerSegment.price * routeDict[(edge.Source, edge.Target)].Segments);
            }

            IEnumerable<IEnumerable<Edge<int>>> shortestPaths;
            try
            {
                shortestPaths = graph.RankedShortestPathHoffmanPavley(
                    EdgeWeights, sr.fromCityId, sr.toCityId, 2);
            }
            catch (KeyNotFoundException e)
            {
                return new List<RouteSearchViewModel>();
            }

            var result = new List<RouteSearchViewModel>();

            var cityDict = _cityRepository.Query().Select().ToDictionary(c => c.Id);
            var transportTypeDict = _transportTypeRepository.Query().Select().ToDictionary(t => t.Id);
            
            foreach (var shortestPath in shortestPaths)
            {
                var routeModel = new RouteSearchViewModel();
                var parts = new List<RouteViewModel>();
                var estimatedTime = 0;
                var price = 0.0;
                
                foreach (var edge in shortestPath)
                {
                    parts.Add(new RouteViewModel
                    {
                        fromCity = cityDict[edge.Source].Name,
                        toCity = cityDict[edge.Target].Name,
                        transportType = transportTypeDict[routeDict[(edge.Source, edge.Target)].TransportType].Name
                    });

                    estimatedTime += routeDict[(edge.Source, edge.Target)].LongHour;
                    price += EdgeWeights(edge);
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
