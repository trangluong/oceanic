using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oceanic.Common.Model;

namespace Oceanic.Core
{
    public class Price: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int MaxWeight { get; set; }
        public double Fee { get; set; }

    }
}
