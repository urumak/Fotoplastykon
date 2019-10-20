using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.Tools.Pager
{
    public static class Pagination
    {
        public static async Task<IPaginationResult<T>> GetPaginationResult<T>(this IQueryable<T> query, IPager pager) where T : class
        {
            pager.TotalRows = query.Count();
            return new PaginationResult<T>
            {
                Pager = pager,
                Items = await query.Skip(pager.PageIndex * pager.PageSize).Take(pager.PageSize).ToListAsync()
            };
        }
    }
}
