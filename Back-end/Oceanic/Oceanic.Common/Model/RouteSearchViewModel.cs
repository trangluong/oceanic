using System.Collections.Generic;

namespace Oceanic.Common.Model
{
    public class RouteSearchViewModel
    {
        public int estimatedTime { get; set; }
        public int price { get; set; }
        public List<RouteViewModel> parts { get; set; }
    }
}
