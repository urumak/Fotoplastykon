using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Storage;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FileService : IFileService
    {
        protected IUnitOfWork Unit { get; }
        protected IStorekeeper Storekeeper { get; }

        public FileService(IUnitOfWork unit, IStorekeeper storekeeper)
        {
            Unit = unit;
            Storekeeper = storekeeper;
        }

        public FileInfo Get(long id)
        {
            var fileInfo = Unit.Files.Get(id);
            return Storekeeper.Get(fileInfo.Name, fileInfo.RelativePath);
        }

        public FileInfo Add(IFormFile file, string relativePath = null)
        {
            var fileContent = GetFileContent(file);
            var storedFile = Storekeeper.Add(fileContent, file.FileName);
            Unit.Files.Add(new StoredFileInfo
            {
                AbsolutePath = storedFile.FullName,
                Name = storedFile.Name,
                PublicId = Guid.NewGuid().ToString(),
                RelativePath = relativePath,
                Size = fileContent.Length
            });
            Unit.Complete();

            return storedFile;
        }

        public void Remove(long id)
        {
            var fileInfo = Unit.Files.Get(id);
            Storekeeper.Remove(fileInfo.Name, fileInfo.RelativePath);
            Unit.Files.Remove(id);
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
