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
        protected IFilesService Files { get; }
        private User _user;

        public AccountService(IUnitOfWork unit, IMapper mapper, IFilesService files)
            : base(unit, mapper)
        {
            Files = files;
        }

        public async Task ChangeProfilePhoto(long userId, IFormFile file)
        {
            var user = await Unit.Users.Get(userId);

            var photoId = await Files.AddAndReturnId(file, "profiles\\" + userId);
            var oldPhotoId = user.PhotoId;

            user.PhotoId = photoId;
            await Unit.Complete();

            if(oldPhotoId.HasValue) await Files.Remove(oldPhotoId.Value);
        }

        public async Task RomoveProfilePhoto(long userId)
        {
            var user = await Unit.Users.Get(userId);

            if (user.PhotoId.HasValue) await Files.Remove(user.PhotoId.Value);
        }
    }
}
