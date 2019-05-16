using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oceanic.Core;
using Oceanic.Infrastructure.Interfaces;
using Oceanic.Services.Interface;

namespace Oceanic.Services.Service
{
    public class SearchService : BaseService<City>, ISearchService
    {
        private readonly IRepositoryAsync<City> _repository;

        public SearchService(IRepositoryAsync<City> repository) : base(repository)
        {
            this._repository = repository;
        }
        public IEnumerable<City> LoadCity()
        {
            return _repository.Query().Select().ToList();
        }

       
    }
}
