using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FilesRepository : Repository<StoredFileInfo>, IFilesRepository
    {
        public FilesRepository(DatabaseContext context)
            : base(context)
        {
        }
    }
}
