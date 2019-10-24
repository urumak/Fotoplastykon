using AutoMapper;
using Fotoplastykon.BLL.DTOs.Search;
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

        public async Task<List<SearchDTO>> Search(string search, int limit = 10)
        {
            var users = await Unit.Users.GetForSearch(search, limit);
            var films = await Unit.Films.GetForSearch(search, limit);
            var filmPeople = await Unit.FilmPeople.GetForSearch(search, limit);

            return Mapper.Map<List<SearchDTO>>(users)
                .Concat(Mapper.Map<List<SearchDTO>>(films))
                .Concat(Mapper.Map<List<SearchDTO>>(filmPeople))
                .OrderBy(m => m.Value)
                .Take(limit)
                .ToList();
        }
    }
}
