using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oceanic.Common.Model;

namespace Oceanic.Core
{
    public class Route: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public int LongHour { get; set; }
        public int TransportType { get; set; }
        public int Segments { get; set; }
        public bool IsActive { get; set; }
    }
}
