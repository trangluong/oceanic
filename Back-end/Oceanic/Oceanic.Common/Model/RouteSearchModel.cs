﻿namespace Oceanic.Common.Model
{
    public class RouteSearchModel : RoutesViewModel
    {
        public string from_city { get; set; }
        public string to_city { get; set; }
        public int hours { get; set; }
        public int segment { get; set; }
        public string transportType { get; set; }

    }
}
