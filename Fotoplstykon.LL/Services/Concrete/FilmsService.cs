using AutoMapper;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmsService : Service, IFilmsService
    {
        public FilmsService(IUnitOfWork unit, IMapper mapper)
            :base(unit, mapper)
        {

        }

        public async Task Rate(FilmMarkDTO mark)
        {
            await Unit.FilmWatchings.Add(Mapper.Map<FilmWatching>(mark));
            await Unit.Complete();
        }

        public async Task<bool> CheckIfWatchingExists(long userId, long filmId)
        {
            return await Unit.FilmWatchings.Get(userId, filmId) != null;
        }

        public async Task<bool> CheckIfExists(long filmId)
        {
            return await Unit.Films.Get(filmId) != null;
        }

        public async Task<FilmPageDTO> GetForPage(long id)
        {
            return Mapper.Map<FilmPageDTO>(await Unit.Films.GetForPage(id));
        }
    }
}
