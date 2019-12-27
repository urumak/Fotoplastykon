using AutoMapper;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
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
            var entity = await Unit.PersonMarks.Get(mark.UserId, mark.PersonId);

            if (entity == null) await Unit.PersonMarks.Add(Mapper.Map<PersonMark>(mark));
            else Mapper.Map(mark, entity);

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

        public async Task<FilmPersonPageDTO> GetForPage(long personId, long userId)
        {
            var person = Mapper.Map<FilmPersonPageDTO>(await Unit.FilmPeople.GetForPage(personId));
            person.ForumThreads = Mapper.Map<List<ForumElementDTO>>(await Unit.ForumThreads.GetTheMostPopularForFilmPerson(personId));

            var userRating = await Unit.PersonMarks.Get(userId, personId);
            if (userRating != null) person.UserRating = userRating.Mark;

            return person;
        }

        public async Task<decimal?> GetRating(long personId)
        {
            return await Unit.PersonMarks.GetRating(personId);
        }

        public async Task<IPaginationResult<RankModel>> GetPaginatedListForUser(IPager pager, long userId)
        {
            var data = await Unit.PersonMarks.GetPaginationResultForUser(pager, userId);
            return new PaginationResult<RankModel>
            {
                Items = Mapper.Map<List<RankModel>>(data.Items),
                Pager = data.Pager
            };
        }
    }
}
