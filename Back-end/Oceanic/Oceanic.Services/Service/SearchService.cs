using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public SearchService(IRepositoryAsync<City> cityRepository, 
            IRepositoryAsync<Route> routeRepository,
            IRepositoryAsync<TransportType> transportTypeRepository) 
        {
            this._cityRepository = cityRepository;
            this._routeRepository = routeRepository;
            this._transportTypeRepository = transportTypeRepository;
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
            var routes = _routeRepository.Query().Select().ToList();

            var graph = buildGraph(routes);

            double EdgeWeights(Edge<int> edge) => 1.0;

            var shortestPaths = graph.RankedShortestPathHoffmanPavley(
                EdgeWeights, sr.fromCityId, sr.toCityId, 2);

            var result = new List<RouteSearchViewModel>();

            var cityDict = _cityRepository.Query().Select().ToDictionary(c => c.Id);
            var transportTypeDict = _transportTypeRepository.Query().Select().ToDictionary(t => t.Id);

            var routeLookup = routes.ToDictionary(r => (r.FromCityId, r.ToCityId));
            
            foreach (var shortestPath in shortestPaths)
            {
                var routeModel = new RouteSearchViewModel();
                var parts = new List<RouteViewModel>();
                var estimatedTime = 0;
                var price = 0;
                
                foreach (var edge in shortestPath)
                {
                    parts.Add(new RouteViewModel
                    {
                        fromCity = cityDict[edge.Source].Name,
                        toCity = cityDict[edge.Target].Name,
                        transportType = transportTypeDict[routeLookup[(edge.Source, edge.Target)].TransportType].Name
                    });

                    estimatedTime += routeLookup[(edge.Source, edge.Target)].LongHour;
                    price += 1;
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
