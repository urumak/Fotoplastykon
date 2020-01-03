using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPeopleInRolesRepository : IRepository<PersonInRole>
    {
        Task<IPaginationResult<PersonInRole>> GetFilmCast(IPager pager, long filmId);
        Task<IPaginationResult<PersonInRole>> GetFilmMakers(IPager pager, long filmId);
        Task<IPaginationResult<PersonInRole>> GetPersonRoles(IPager pager, long personId);
    }
}
