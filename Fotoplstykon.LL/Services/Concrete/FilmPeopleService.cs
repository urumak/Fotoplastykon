using AutoMapper;
using Fotoplastykon.BLL.Models.FilmPeople;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmPeopleService : Service, IFilmPeopleService
    {
        public FilmPeopleService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {

        }

        public void Rate(PersonMarkModel mark)
        {
            Unit.PersonMarks.Add(Mapper.Map<PersonMark>(mark));
            Unit.Complete();
        }

        public bool CheckIfWatchingExists(long userId, long personId)
        {
            return Unit.PersonMarks.Get(userId, personId) != null;
        }

        public bool CheckIfExists(long personId)
        {
            return Unit.FilmPeople.Get(personId) != null;
        }
    }
}
