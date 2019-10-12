using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.Pager
{
    public interface IPaginationResult<T> where T : class
    {
        IPager Pager { get; set; }
        IEnumerable<T> Records { get; set; }
    }
}
