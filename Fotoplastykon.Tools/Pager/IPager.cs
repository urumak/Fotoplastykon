using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.Pager
{
    public interface IPager
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int TotalRows { get; set; }
        int TotalPages { get; }
    }
}
