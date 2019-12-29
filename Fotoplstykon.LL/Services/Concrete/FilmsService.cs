using AutoMapper;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.Storage;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Fotoplastykon.Tools.Extensions;
using System.Linq;
using LinqKit;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmsService : Service, IFilmsService
    {
        protected IStorekeeper Storekeeper { get; }

        public FilmsService(IUnitOfWork unit, IMapper mapper, IStorekeeper storekeeper)
            :base(unit, mapper)
        {
            Storekeeper = storekeeper;
        }

        #region GetPaginatedList()
        public async Task<IPaginationResult<FilmListItem>> GetPaginatedList(IPager pager)
        {
            var predicate = PredicateBuilder.New<Film>(true);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(i => i.Title.Contains(pager.Search));

            var data = await Unit.Films.GetPaginatedList(
                pager,
                predicate,
                i => i.Title,
                OrderDirection.ASC);

            return new PaginationResult<FilmListItem>
            {
                Items = Mapper.Map<List<FilmListItem>>(data.Items),
                Pager = data.Pager
            };
        }
        #endregion

        #region Rate()
        public async Task Rate(FilmMarkDTO mark)
        {
            var entity = await Unit.FilmWatchings.Get(mark.UserId, mark.FilmId);

            if (entity == null) await Unit.FilmWatchings.Add(Mapper.Map<FilmWatching>(mark));
            else Mapper.Map(mark, entity);

            await Unit.Complete();
        }
        #endregion

        #region CheckIfWatchingExists()
        public async Task<bool> CheckIfWatchingExists(long userId, long filmId)
        {
            return await Unit.FilmWatchings.Get(userId, filmId) != null;
        }
        #endregion

        #region CheckIfExists()
        public async Task<bool> CheckIfExists(long filmId)
        {
            return await Unit.Films.Get(filmId) != null;
        }
        #endregion

        #region GetForPage()
        public async Task<FilmPageDTO> GetForPage(long filmId, long userId)
        {
            var film = Mapper.Map<FilmPageDTO>(await Unit.Films.GetForPage(filmId));
            film.ForumThreads = Mapper.Map<List<ForumElementDTO>>(await Unit.ForumThreads.GetTheMostPopularForFilm(filmId));

            var userRating = await Unit.FilmWatchings.Get(userId, filmId);
            if (userRating != null) film.UserRating = userRating.Mark;

            return film;
        }
        #endregion

        #region GetRating()
        public async Task<decimal?> GetRating(long filmId)
        {
            return await Unit.FilmWatchings.GetRating(filmId);
        }
        #endregion

        #region Fetch()
        public async Task<FilmFormModel> Fetch(long id)
        {
            return Mapper.Map<FilmFormModel>(await Unit.Films.GetWithPeopleInRoles(id));
        }
        #endregion

        #region GetPaginatedListForUser()
        public async Task<IPaginationResult<RankModel>> GetPaginatedListForUser(IPager pager, long userId)
        {
            var data = await Unit.FilmWatchings.GetPaginationResultForUser(pager, userId);
            return new PaginationResult<RankModel>
            {
                Items = Mapper.Map<List<RankModel>>(data.Items),
                Pager = data.Pager
            };
        }
        #endregion

        #region Update()
        public async Task Update(long id, FilmFormModel model)
        {
            var entity = await Unit.Films.Get(id);
            Mapper.Map(model, entity);

            var (rolesToAdd, rolesToUpdate, rolesToDelete) = await SortPeopleInRolesToUpdate(id, model.People);
            await AddPeopleInRoles(id, rolesToAdd);
            await UpdatePeopleInRoles(id, rolesToUpdate);
            DeletePeopleInRoles(rolesToDelete);

            await Unit.Complete();
        }

        private async Task<(List<PersonInRoleFormModel>, List<PersonInRoleFormModel>, List<PersonInRole>)> 
            SortPeopleInRolesToUpdate(long filmId, List<PersonInRoleFormModel> peopleModel)
        {
            var peopleInRoles = await Unit.PeopleInRoles.Find(r => r.FilmId == filmId);

            return (peopleModel.Where(x => !peopleInRoles.Select(r => r.Id).Contains(x.Id)).ToList(), 
                peopleModel.Where(x => peopleInRoles.Select(r => r.Id).Contains(x.Id)).ToList(),
                peopleInRoles.Where(x => !peopleModel.Select(r => r.Id).Contains(x.Id)).ToList());
        }

        private async Task AddPeopleInRoles(long filmId, List<PersonInRoleFormModel> peopleModel)
        {
            var items = Mapper.Map<List<PersonInRole>>(peopleModel);
            items.ForEach(x => x.FilmId = filmId);
            await Unit.PeopleInRoles.AddRange(items);
        }

        private async Task UpdatePeopleInRoles(long filmId, List<PersonInRoleFormModel> peopleModel)
        {
            var entities = await Unit.PeopleInRoles.Find(x => peopleModel.Select(m => m.Id).Contains(x.Id));
            var peopleDictionary = peopleModel.ToDictionary(x => x.Id, x => x);

            foreach (var entity in entities)
            {
                Mapper.Map(peopleDictionary[entity.Id], entity);
                entity.FilmId = filmId;
            }
        }

        private void DeletePeopleInRoles(List<PersonInRole> peopleToDelete)
        {
            Unit.PeopleInRoles.RemoveRange(peopleToDelete);
        }
        #endregion

        #region Remove()
        public async Task Remove(long id)
        {
            await Unit.Films.Remove(id);
            await Unit.Complete();
        }
        #endregion

        #region GetFilmmakers()
        public async Task<List<FilmPersonDropDownModel>> GetFilmmakers(string search, int limit = 10)
        {
            return Mapper.Map<List<FilmPersonDropDownModel>>(await Unit.FilmPeople.GetForSearch(search, limit));
        }
        #endregion

        #region GetForSearch()
        public async Task<FilmPersonDropDownModel> GetForSearch(long id)
        {
            return Mapper.Map<FilmPersonDropDownModel>(await Unit.FilmPeople.GetForSearch(id));
        }
        #endregion

        #region GetRoleTypes()
        public List<RoleTypeDictionary> GetRoleTypes()
        {
            var result = new List<RoleTypeDictionary>();

            foreach(var item in Enum.GetValues(typeof(RoleType)).Cast<RoleType>())
            {
                result.Add(new RoleTypeDictionary
                {
                    Key = (int)item,
                    Value = item.GetDescription()
                });
            }

            return result;
        }
        #endregion

        #region ChangePhoto()
        public async Task ChangePhoto(long id, IFormFile file)
        {
            var film = await Unit.Films.Get(id);

            var photoId = await AddFile(file, "films\\");
            var oldPhotoId = film.PhotoId;

            film.PhotoId = photoId;
            await Unit.Complete();

            if (oldPhotoId.HasValue) await RemoveFile(oldPhotoId.Value);
        }

        private async Task RemoveFile(long id)
        {
            var fileInfo = await Unit.Files.Get(id);
            Storekeeper.Remove(fileInfo.UniqueName, fileInfo.RelativePath);
            await Unit.Files.Remove(id);
            await Unit.Complete();
        }

        private async Task<long> AddFile(IFormFile file, string relativePath = null)
        {
            var fileContent = GetFileContent(file);
            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var storedFile = Storekeeper.Add(fileContent, uniqueName, relativePath);

            var storedFileInfo = await Unit.Files.Add(new StoredFileInfo
            {
                DisplayName = file.FileName,
                PublicId = Guid.NewGuid().ToString(),
                UniqueName = uniqueName,
                MimeType = file.ContentType,
                RelativePath = relativePath,
                Size = fileContent.Length
            });
            await Unit.Complete();

            return storedFileInfo.Id;
        }

        private byte[] GetFileContent(IFormFile file)
        {
            byte[] fileContent;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                fileContent = stream.ToArray();
            };

            return fileContent;
        }
        #endregion
    }
}
