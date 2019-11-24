using AutoMapper;
using Fotoplastykon.BLL.DTOs.Search;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class SearchService : Service, ISearchService
    {
        public SearchService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<List<SearchDTO>> Search(string search, int limit = 10)
        {
            var (users, films, filmPeople) = await GetSearchItems(search, limit);
            var items = ConcatSearchList((users, films, filmPeople), limit);
            var result = items.Where(x => x.Value.StartsWith(search)).OrderBy(x => x.Value);

            return result.Concat(items.Where(x => !x.Value.StartsWith(search))
                .OrderBy(x => x.Value))
                .ToList();
        }

        private async Task<(List<User>, List<Film>, List<FilmPerson>)> GetSearchItems(string search, int limit)
        {
            return (await Unit.Users.GetForSearch(search, limit),
                await Unit.Films.GetForSearch(search, limit),
                await Unit.FilmPeople.GetForSearch(search, limit));
        }

        private List<SearchDTO> ConcatSearchList((List<User> users, List<Film> films, List<FilmPerson> filmPeople) items, int limit)
        {
            return Mapper.Map<List<SearchDTO>>(items.users)
                .Concat(Mapper.Map<List<SearchDTO>>(items.films))
                .Concat(Mapper.Map<List<SearchDTO>>(items.filmPeople))
                .OrderBy(m => m.Value)
                .Take(limit)
                .ToList();
        }
    }
}
