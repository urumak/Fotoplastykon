using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.Tools.InfiniteScroll
{
    public static class InfiniteScrollDataLoader
    {
        public static async Task<IInfiniteScrollResult<T>> GetInfiniteScrollResult<T>(this IQueryable<T> query, IInfiniteScroll scroll) where T : class
        {
            return new InfiniteScrollResult<T>
            {
                Scroll = scroll,
                Items = await query.Skip(scroll.RowsLoaded).Take(scroll.UnitSize).ToListAsync()
            };
        }
    }
}
