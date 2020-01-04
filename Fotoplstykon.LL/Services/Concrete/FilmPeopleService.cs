using AutoMapper;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.Storage;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using LinqKit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmPeopleService : Service, IFilmPeopleService
    {
        protected IFilesService Files { get; }

        public FilmPeopleService(IUnitOfWork unit, IMapper mapper, IFilesService files)
            : base(unit, mapper)
        {
            Files = files;
        }

        #region GetList()
        public async Task<IPaginationResult<FilmPersonListItem>> GetList(IPager pager)
        {
            var predicate = PredicateBuilder.New<FilmPerson>(true);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(i => i.FirstName.Contains(pager.Search) || i.Surname.Contains(pager.Search));

            var data = await Unit.FilmPeople.GetPaginatedList(
                pager,
                predicate,
                i => i.FirstName,
                OrderDirection.ASC);

            return new PaginationResult<FilmPersonListItem>
            {
                Items = Mapper.Map<List<FilmPersonListItem>>(data.Items),
                Pager = data.Pager
            };
        }
        #endregion

        #region Fetch()
        public async Task<FilmPersonFormModel> Fetch(long id)
        {
            return Mapper.Map<FilmPersonFormModel>(await Unit.FilmPeople.Get(id));
        }
        #endregion

        #region Remove()
        public async Task Remove(long id)
        {
            await Unit.FilmPeople.Remove(id);
            await Unit.Complete();
        }
        #endregion

        #region Update()
        public async Task Update(long id, FilmPersonFormModel model)
        {
            var entity = await Unit.FilmPeople.Get(id);
            Mapper.Map(model, entity);

            await Unit.Complete();
        }
        #endregion

        #region Add()
        public async Task<long> Add(FilmPersonFormModel model)
        {
            var entity = Mapper.Map<FilmPerson>(model);
            entity.PublicId = Guid.NewGuid().ToString();
            await Unit.FilmPeople.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }
        #endregion

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

            var userRating = await Unit.PersonMarks.Get(userId, personId);
            if (userRating != null) person.UserRating = userRating.Mark;

            return person;
        }

        public async Task<IPaginationResult<RoleInFilmDTO>> GetPersonRoles(IPager pager, long personId)
        {
            var data = await Unit.PeopleInRoles.GetPersonRoles(pager, personId);

            return new PaginationResult<RoleInFilmDTO>
            {
                Items = Mapper.Map<List<RoleInFilmDTO>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task<IPaginationResult<ForumElementDTO>> GetTheMostPopularForumThreads(IPager pager, long personId)
        {
            var data = await Unit.ForumThreads.GetTheMostPopularForFilmPerson(pager, personId);

            return new PaginationResult<ForumElementDTO>
            {
                Items = Mapper.Map<List<ForumElementDTO>>(data.Items),
                Pager = data.Pager
            };
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

        #region ChangePhoto()
        public async Task ChangePhoto(long id, IFormFile file)
        {
            var person = await Unit.FilmPeople.Get(id);

            var photoId = await Files.AddAndReturnId(file, "filmPeople\\");
            var oldPhotoId = person.PhotoId;

            person.PhotoId = photoId;
            await Unit.Complete();

            if (oldPhotoId.HasValue) await Files.Remove(oldPhotoId.Value);
        }
        #endregion
    }
}
