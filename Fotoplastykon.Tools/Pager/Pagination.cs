using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.Tools.Pager
{
    public static class Pagination
    {
        public static IPaginationResult<T> GetPaginationResult<T>(this IQueryable<T> query, IPager pager) where T : class
        {
            pager.TotalRows = query.Count();
            return new PaginationResult<T>
            {
                Pager = pager,
                Records = query.Skip(pager.PageIndex * pager.PageSize).Take(pager.PageSize).ToList()
            };
        }
    }
}
