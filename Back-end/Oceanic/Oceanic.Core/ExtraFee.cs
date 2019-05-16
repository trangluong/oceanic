using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oceanic.Common.Model;

namespace Oceanic.Core
{
    public class ExtraFee: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GoodsTypeId { get; set; }
        public double ExtraPercent { get; set; }
    }
}
