using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oceanic.ViewModel
{
    public class RouteSearchViewModel
    {
        public int estimatedTime { get; set; }
        public int price { get; set; }
        public List<RouteViewModel> parts { get; set; }
    }
}
