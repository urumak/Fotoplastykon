using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public class InfiniteScroll : IInfiniteScroll
    {
        public int UnitSize { get; set; }
        public int RowsLoaded { get; set; }
    }
}
