using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class SearchService : ISearchService
    {
        private readonly IRepositoryAsync<City> _cityRepository;
        private readonly IRepositoryAsync<Route> _routeRepository;
        public SearchService(IRepositoryAsync<City> cityRepository, IRepositoryAsync<Route> routeRepository) 
        {
            this._cityRepository = cityRepository;
            this._routeRepository = routeRepository;
        }
        public IEnumerable<City> LoadCity()
        {
            return _cityRepository.Query().Select().ToList();
        }

       

    }
}
