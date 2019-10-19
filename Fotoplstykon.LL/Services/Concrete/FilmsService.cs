using AutoMapper;
using Fotoplastykon.BLL.Models.Films;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmsService : Service, IFilmsService
    {
        public FilmsService(IUnitOfWork unit, IMapper mapper)
            :base(unit, mapper)
        {

        }

        public void Rate(FilmMarkModel mark)
        {
            Unit.FilmWatchings.Add(Mapper.Map<FilmWatching>(mark));
            Unit.Complete();
        }

        public bool CheckIfWatchingExists(long userId, long filmId)
        {
            return Unit.FilmWatchings.Get(userId, filmId) != null;
        }

        public bool CheckIfExists(long filmId)
        {
            return Unit.Films.Get(filmId) != null;
        }
    }
}
