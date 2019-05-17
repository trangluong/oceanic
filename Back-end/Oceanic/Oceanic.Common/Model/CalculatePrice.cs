using Oceanic.Common.Enum;
using Oceanic.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oceanic.Common.Model
{
    public class CalculatePrice
    {
        public decimal price { get; set; }
        public int status { get; set; }

    }
}
