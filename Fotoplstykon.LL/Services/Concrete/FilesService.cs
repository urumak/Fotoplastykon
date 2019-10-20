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
    public class FilesService : Service, IFilesService
    {
        protected IStorekeeper Storekeeper { get; }

        public FilesService(IUnitOfWork unit, IMapper mapper, IStorekeeper storekeeper)
            : base(unit, mapper)
        {
            Storekeeper = storekeeper;
        }

        public async Task<FileInfo> Get(long id)
        {
            var fileInfo = await Unit.Files.Get(id);
            if (fileInfo == null) return null;
            return Storekeeper.Get(fileInfo.Name, fileInfo.RelativePath);
        }

        public async Task<FileInfo> Add(IFormFile file, string relativePath = null)
        {
            var fileContent = GetFileContent(file);
            var storedFile = Storekeeper.Add(fileContent, file.FileName);
            await Unit.Files.Add(new StoredFileInfo
            {
                AbsolutePath = storedFile.FullName,
                Name = storedFile.Name,
                PublicId = Guid.NewGuid().ToString(),
                RelativePath = relativePath,
                Size = fileContent.Length
            });
            await Unit.Complete();

            return storedFile;
        }

        public async Task Remove(long id)
        {
            var fileInfo = await Unit.Files.Get(id);
            Storekeeper.Remove(fileInfo.Name, fileInfo.RelativePath);
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
