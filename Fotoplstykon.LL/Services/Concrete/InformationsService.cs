using AutoMapper;
using Fotoplastykon.BLL.DTOs.Information;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class InformationsService : Service, IInformationsService
    {
        protected IFilesService Files{ get; }
        public InformationsService(IUnitOfWork unit, IMapper mapper, IFilesService files)
            : base (unit, mapper)
        {
            Files = files;
        }

        public async Task<IPaginationResult<ListItem>> GetPaginatedList(IPager pager)
        {
            var predicate = PredicateBuilder.New<Information>(true);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(i => i.Title.Contains(pager.Search));

            var filteredData = await Unit.Informations.GetPaginatedList(
                pager,
                predicate,
                i => i.DateCreated,
                OrderDirection.DESC);

            return new PaginationResult<ListItem>
            {
                Items = Mapper.Map<List<ListItem>>(filteredData.Items),
                Pager = filteredData.Pager
            };
        }

        public async Task<InformationFormModel> Fetch(long id)
        {
            return Mapper.Map<InformationFormModel>(await Unit.Informations.Get(id));
        }

        public async Task<IEnumerable<ListItem>> GetListForMainPage(int limit = 5)
        {
            return Mapper.Map<IEnumerable<ListItem>>(await Unit.Informations.GetForMainPage(limit));
        }

        public async Task<InformationDTO> GetWithCreatorAndComments(long id)
        {
            var information = Mapper.Map<InformationDTO>(await Unit.Informations.GetWithCreator(id));
            information.Comments = Mapper.Map<List<CommentDTO>>(await Unit.InformationComments.GetList(id));

            return information;
        }

        public async Task<long> AddComment(CommentDTO comment, long userId)
        {
            var entity = Mapper.Map<InformationComment>(comment);
            entity.CreatedById = userId;
            entity = await Unit.InformationComments.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }
        public async Task RemoveComment(long id)
        {
            await Unit.InformationComments.Remove(id);
            await Unit.Complete();
        }

        public async Task UpdateComment(long id, CommentDTO comment)
        {
            var entity = await Unit.InformationComments.Get(id);
            Mapper.Map(comment, entity);
            await Unit.Complete();
        }

        public async Task<bool> CheckIfCommentExists(long id)
        {
            return await Unit.InformationComments.Get(id) != null;
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.Informations.Get(id) != null;
        }

        public async Task<List<InformationComment>> GetComments(long informationId)
        {
            return Mapper.Map<List<InformationComment>>(await Unit.InformationComments.GetList(informationId));
        }

        public async Task<InformationComment> GetComment(long id)
        {
            return await Unit.InformationComments.Get(id);
        }

        #region Add()
        public async Task<long> Add(InformationFormModel model, long userId)
        {
            var entity = Mapper.Map<Information>(model);
            entity.CreatedById = userId;
            await Unit.Informations.Add(entity);
            await Unit.Complete();

            return entity.Id;
        }
        #endregion

        public async Task Update(long id, InformationFormModel model)
        {
            var entity = await Unit.Informations.Get(id);
            Mapper.Map(model, entity);

            await Unit.Complete();
        }

        public async Task Remove(long id)
        {
            await Unit.Informations.Remove(id);
            await Unit.Complete();
        }

        public async Task ChangePhoto(long id, IFormFile file)
        {
            var information = await Unit.Informations.Get(id);

            var photoId = await Files.AddAndReturnId(file, "information\\");
            var oldPhotoId = information.PhotoId;

            information.PhotoId = photoId;
            await Unit.Complete();

            if (oldPhotoId.HasValue) await Files.Remove(oldPhotoId.Value);
        }
    }
}
