using Fotoplastykon.BLL.DTOs.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFilesService
    {
        Task<FileDTO> Get(long id);
        Task<FileInfo> Add(IFormFile file, string relativePath = null);
        Task Remove(long id);
        Task<bool> CheckIfExists(long id);
        Task<long> AddAndReturnId(IFormFile file, string relativePath = null);
    }
}
