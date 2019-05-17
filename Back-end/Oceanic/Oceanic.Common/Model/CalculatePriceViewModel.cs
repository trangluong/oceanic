using System;
using System.Collections.Generic;
using System.Text;

namespace Oceanic.Common.Model
{
    public class CalculatePriceViewModel
    {
        public int weight { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int length { get; set; }
        public string goods_type { get; set; }
        public String departure_date { get; set; }
    }
}
