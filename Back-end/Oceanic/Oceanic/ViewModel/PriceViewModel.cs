using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oceanic
{
    public class PriceViewModel
    {
        public int id { get; set; }
        public string sizeType { get; set; }
        public decimal price { get; set; }
        public int maxWeight { get; set; }
    }
}
