using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Fotoplastykon.BLL.DTOs.Users;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.BLL.Helpers;
using Fotoplastykon.DAL.Storage;
using System;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class UsersService : Service, IUsersService
    {
        private IPasswordHasher<User> Hasher { get; }
        private Anonymiser<User> Anonymiser { get; }
        private IStorekeeper Storekeeper { get; }

        public UsersService(IUnitOfWork unit, IMapper mapper, IPasswordHasher<User> hasher, Anonymiser<User> anonymiser, IStorekeeper storekeeper)
            : base(unit, mapper)
        {
            Hasher = hasher;
            Anonymiser = anonymiser;
            Storekeeper = storekeeper;
        }

        public async Task<User> Get(long id)
        {
            return await Unit.Users.Get(id);
        }

        public async Task<bool> Add(AddUserDTO user)
        {
            var entity = Mapper.Map<User>(user);
            entity.PublicId = Guid.NewGuid().ToString();

            await Unit.Users.Add(entity);
            await Unit.Complete();

            var result = await SetPassword(entity.Id, user.Password);
            await Unit.Complete();

            return result;
        }

        public async Task<UserLoginDTO> GetForLoginByUserName(string userName)
        {
            var user = await Unit.Users.GetByUserName(userName);
            return Mapper.Map<UserLoginDTO>(user);
        }

        public async Task<List<User>> Search(string searchString)
        {
            //TODO: poprawić wyszukiwanie tak, żeby dało się szukać po imieniu i nazwisku
            var users = await Unit.Users
                .Find(u => u.UserName.Contains(searchString)
                    || u.FirstName.Contains(searchString)
                    || u.Surname.Contains(searchString));

            return users.ToList();
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.Users.Get(id) != null;
        }

        public async Task Anonymise(long id)
        {
            var user = await Unit.Users.Get(id);

            if(user.PhotoId.HasValue)
            {
                var file = await Unit.Files.Get(user.PhotoId.Value);
                Storekeeper.Remove(file.DisplayName, file.RelativePath);
                await Unit.Files.Remove(user.PhotoId.Value);
            }

            user = Anonymiser.Anonymise(user);
            await Unit.Complete();
        }

        public async Task<UserPageDTO> GetForPage(long id)
        {
            return Mapper.Map<UserPageDTO>(await Unit.Users.GetForPage(id));
        }

        private async Task<bool> SetPassword(long id, string password)
        {
            var user = await Unit.Users.Get(id);

            if (user == null) return false;

            user.PasswordHash = Hasher.HashPassword(user, password);

            return true;
        }
    }
}
