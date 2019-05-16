﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("api/city")]
        [HttpGet]
        public IEnumerable<CityViewModel> LoadCities()
        {
            return _searchRoutesService.LoadCity().Select(x => new CityViewModel
            {
                Code = x.Code,
                Name = x.Name
            });
        }

    }
}