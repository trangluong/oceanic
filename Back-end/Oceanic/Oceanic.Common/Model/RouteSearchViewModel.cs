using System.Collections.Generic;

namespace Oceanic.Common.Model
{
    public class RouteSearchViewModel
    {
        public int estimatedTime { get; set; }
        public double price { get; set; }
        public List<RouteViewModel> parts { get; set; }
    }
}
