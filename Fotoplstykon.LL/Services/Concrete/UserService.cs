using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.LL.Models.Users;
using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Fotoplastykon.LL.Services.Concrete
{
    public class UserService : IUserService
    {
        private IUsersUnit UsersUnit { get; }
        private IMapper Mapper { get; }
        private IPasswordHasher<User> Hasher { get; }

        public UserService (IUsersUnit usersUnit, IMapper mapper, IPasswordHasher<User> hasher)
        {
            UsersUnit = usersUnit;
            Mapper = mapper;
            Hasher = hasher;
        }

        public IEnumerable<AddUserModel> GetAll()
        {
            return Mapper.Map<IEnumerable<AddUserModel>>(UsersUnit.Users.GetAll());
        }

        public bool Add(AddUserModel user)
        {
            var entity = Mapper.Map<User>(user);

            UsersUnit.Users.Add(entity);
            UsersUnit.Complete();

            var result = SetPassword(entity.Id, user.Password);
            UsersUnit.Complete();

            return result;
        }

        private bool SetPassword(long id, string password)
        {
            var user = UsersUnit.Users.Get(id);

            if (user == null) return false;

            user.PasswordHash = Hasher.HashPassword(user, password);

            return true;
        }
    }
}
