using AutoMapper;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmPeopleService : Service, IFilmPeopleService
    {
        public FilmPeopleService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {

        }

        public async Task Rate(PersonMarkDTO mark)
        {
            await Unit.PersonMarks.Add(Mapper.Map<PersonMark>(mark));
            await Unit.Complete();
        }

        public async Task<bool> CheckIfWatchingExists(long userId, long personId)
        {
            return await Unit.PersonMarks.Get(userId, personId) != null;
        }

        public async Task<bool> CheckIfExists(long personId)
        {
            return await Unit.FilmPeople.Get(personId) != null;
        }

        public async Task<FilmPersonPageDTO> GetForPage(long id)
        {
            return Mapper.Map<FilmPersonPageDTO>(await Unit.FilmPeople.GetForPage(id));
        }
    }
}
