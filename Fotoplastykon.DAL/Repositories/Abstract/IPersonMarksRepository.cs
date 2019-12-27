using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPersonMarksRepository : IRepository<PersonMark>
    {
        Task<PersonMark> Get(long userId, long personId);
        Task<decimal?> GetRating(long personId);
        Task<IPaginationResult<PersonMark>> GetPaginationResultForUser(IPager pager, long userId);
    }
}
