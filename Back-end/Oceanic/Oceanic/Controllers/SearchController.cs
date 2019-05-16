using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Oceanic.Common.Model;
using Oceanic.Services.Interface;

namespace Oceanic.Controllers
{
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        //private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly ISearchService _searchRoutesService;

        public SearchController(ISearchService searchRoutesService)
        {
            _searchRoutesService = searchRoutesService;
        }

        [Route("api/routes/search")]
        [HttpPost]
        public List<RouteSearchViewModel> SearchRoutes([FromBody] RouteSearchRequest searchRequest)
        {
            return _searchRoutesService.SearchRoutes(searchRequest);
        }

    }
}