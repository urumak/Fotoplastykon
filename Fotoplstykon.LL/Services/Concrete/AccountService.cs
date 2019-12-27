using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Storage;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class AccountService : Service, IAccountService
    {
        protected IStorekeeper Storekeeper { get; }
        private User _user;

        public AccountService(IUnitOfWork unit, IMapper mapper, IStorekeeper storekeeper)
            : base(unit, mapper)
        {
            Storekeeper = storekeeper;
        }

        public async Task ChangeProfilePhoto(long userId, IFormFile file)
        {
            _user = await Unit.Users.Get(userId);

            var photoId = await AddFile(file, "profiles\\" + userId);
            var oldPhotoId = _user.PhotoId;

            _user.PhotoId = photoId;
            await Unit.Complete();

            if (oldPhotoId.HasValue) await RemoveFile(oldPhotoId.Value);
        }

        public async Task RomoveProfilePhoto(long userId)
        {
            _user = await Unit.Users.Get(userId);

            if (_user.PhotoId.HasValue) await RemoveFile(_user.PhotoId.Value);
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
