using Oceanic.Common.Enum;
using Oceanic.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oceanic.Common.Model
{
    public class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }

    }
}
