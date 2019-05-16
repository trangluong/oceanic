using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oceanic.Common.Model;

namespace Oceanic.Core
{
    public class Size: Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public int MaxHeight { get; set; }
        public int MaxDepth { get; set; }
        public int MaxBreath { get; set; }
    }
}
