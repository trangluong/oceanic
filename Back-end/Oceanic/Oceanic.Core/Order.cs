using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oceanic.Common.Model;

namespace Oceanic.Core
{
    public class Order: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string FromCityId { get; set; }
        public string ToCityId { get; set; }
        public string GoodsTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public double TotalFee { get; set; }
        public string RouteIDs { get; set; }
    }
}
