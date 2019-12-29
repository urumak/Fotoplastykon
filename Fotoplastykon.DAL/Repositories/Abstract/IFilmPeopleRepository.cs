using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IFilmPeopleRepository : IRepository<FilmPerson>
    {
        Task<FilmPerson> GetForSearch(long id);
        Task<List<FilmPerson>> GetForSearch(string search, int limit = 10);
        Task<FilmPerson> GetForPage(long id);
    }
}
