using System.Collections.Generic;
using Oceanic.Core;

namespace Oceanic.Services.Interface
{
    public interface ISizeService : IBaseService<Size>
    {
        IEnumerable<Size> LoadSize();

    }
}
