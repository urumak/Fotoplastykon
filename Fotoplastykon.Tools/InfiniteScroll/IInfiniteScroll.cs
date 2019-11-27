using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public interface IInfiniteScroll
    {
        int UnitSize { get; set; }
        int RowsLoaded { get; set; }
    }
}
