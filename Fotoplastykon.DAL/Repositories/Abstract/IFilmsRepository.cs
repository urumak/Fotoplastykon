using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmsRepository : IRepository<Film>
    {
        Task<List<Film>> GetForSearch(string search, int limit = 10);
        Task<Film> GetForPage(long id);
        Task<Film> GetWithPeopleInRoles(long id);
    }
}
