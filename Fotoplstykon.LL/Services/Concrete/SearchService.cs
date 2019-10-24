using AutoMapper;
using Fotoplastykon.BLL.Models.Search;
using Fotoplastykon.BLL.Services.Abstract;
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

        public async Task<List<SearchModel>> Search(string search, int limit = 10)
        {
            var users = await Unit.Users.GetForSearch(search, limit);
            var films = await Unit.Films.GetForSearch(search, limit);
            var filmPeople = await Unit.FilmPeople.GetForSearch(search, limit);

            return Mapper.Map<List<SearchModel>>(users)
                .Concat(Mapper.Map<List<SearchModel>>(films))
                .Concat(Mapper.Map<List<SearchModel>>(filmPeople))
                .OrderBy(m => m.Value)
                .Take(limit)
                .ToList();
        }
    }
}
