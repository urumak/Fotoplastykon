using AutoMapper;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Shared;
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
            var entity = await Unit.FilmWatchings.Get(mark.UserId, mark.FilmId);

            if (entity == null) await Unit.FilmWatchings.Add(Mapper.Map<FilmWatching>(mark));
            else Mapper.Map(mark, entity);

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

        public async Task<FilmPageDTO> GetForPage(long filmId, long userId)
        {
            var film = Mapper.Map<FilmPageDTO>(await Unit.Films.GetForPage(filmId));
            film.ForumThreads = Mapper.Map<List<ForumElementDTO>>(await Unit.ForumThreads.GetTheMostPopularForFilm(filmId));

            var userRating = await Unit.FilmWatchings.Get(userId, filmId);
            if (userRating != null) film.UserRating = userRating.Mark;

            return film;
        }

        public async Task<decimal?> GetRating(long filmId)
        {
            return await Unit.FilmWatchings.GetRating(filmId);
        }
    }
}
