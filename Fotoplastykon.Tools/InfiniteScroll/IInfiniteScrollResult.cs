using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public interface IInfiniteScrollResult<T> where T : class
    {
        IInfiniteScroll Scroll { get; set; }
        List<T> Items { get; set; }
    }
}
