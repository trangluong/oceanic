using System;
using System.Collections.Generic;
using System.Text;
using Oceanic.Core;

namespace Oceanic.Services.Interface
{
    public interface ISearchService : IBaseService<City>
    {
        IEnumerable<City> LoadCity();

    }
}
