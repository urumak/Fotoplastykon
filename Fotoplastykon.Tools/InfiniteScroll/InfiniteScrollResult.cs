using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public class InfiniteScrollResult<T> : IInfiniteScrollResult<T> where T : class
    {
        public IInfiniteScroll Scroll { get; set; }
        public List<T> Items { get; set; }
    }
}
