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
        protected IStorekeeper Storekeeper{ get; }
        public InformationsService(IUnitOfWork unit, IMapper mapper, IStorekeeper storekeeper)
            : base (unit, mapper)
        {
            Storekeeper = storekeeper;
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

            var photoId = await AddFile(file, "information\\");
            var oldPhotoId = information.PhotoId;

            information.PhotoId = photoId;
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
    }
}
