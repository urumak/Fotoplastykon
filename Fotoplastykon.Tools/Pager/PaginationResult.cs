using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.Pager
{
    public class PaginationResult<T> : IPaginationResult<T> where T : class
    {
        public IPager Pager { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
