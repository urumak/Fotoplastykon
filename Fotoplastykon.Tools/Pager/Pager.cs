using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.Tools.Pager
{
    public class Pager : IPager
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get => PageSize > 0 ? Convert.ToInt32(Math.Ceiling((double)TotalRows / PageSize)) : 0; }
    }
}
