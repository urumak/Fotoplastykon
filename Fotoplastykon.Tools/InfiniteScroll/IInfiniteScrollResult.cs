using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public interface IInfiniteScrollResult<T> where T : class
    {
        IInfiniteScroll Scroll { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}
