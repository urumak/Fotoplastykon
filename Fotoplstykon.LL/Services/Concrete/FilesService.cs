using AutoMapper;
using Fotoplastykon.BLL.DTOs.Files;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Storage;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilesService : Service, IFilesService
    {
        protected IStorekeeper Storekeeper { get; }

        public FilesService(IUnitOfWork unit, IMapper mapper, IStorekeeper storekeeper)
            : base(unit, mapper)
        {
            Storekeeper = storekeeper;
        }

        public async Task<FileDTO> Get(long id)
        {
            var fileInfo = await Unit.Files.Get(id);
            if (fileInfo == null) return null;

            var result = Mapper.Map<FileDTO>(fileInfo);
            result.Content = Storekeeper.GetAllBytes(fileInfo.UniqueName, fileInfo.RelativePath);

            return result;
        }

        public async Task<FileInfo> Add(IFormFile file, string relativePath = null)
        {
            var fileContent = GetFileContent(file);
            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var storedFile = Storekeeper.Add(fileContent, uniqueName, relativePath);

            await Unit.Files.Add(new StoredFileInfo
            {
                DisplayName = file.FileName,
                PublicId = Guid.NewGuid().ToString(),
                UniqueName = uniqueName,
                MimeType = file.ContentType,
                RelativePath = relativePath,
                Size = fileContent.Length
            });
            await Unit.Complete();

            return storedFile;
        }

        public async Task<long> AddAndReturnId(IFormFile file, string relativePath = null)
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

        public async Task Remove(long id)
        {
            var fileInfo = await Unit.Files.Get(id);
            Storekeeper.Remove(fileInfo.UniqueName, fileInfo.RelativePath);
            await Unit.Files.Remove(id);
            await Unit.Complete();
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.Files.Get(id) != null;
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
