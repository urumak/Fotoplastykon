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
        public UserService (IUnitOfWork unit, IMapper mapper, IPasswordHasher<User> hasher)
        {
            Unit = unit;
            Mapper = mapper;
            Hasher = hasher;
        }

        private IUnitOfWork Unit { get; }
        private IMapper Mapper { get; }
        private IPasswordHasher<User> Hasher { get; }

        public IEnumerable<AddUserModel> GetAll()
        {
            return Mapper.Map<IEnumerable<AddUserModel>>(Unit.Users.GetAll());
        }

        public bool Add(AddUserModel user)
        {
            var entity = Mapper.Map<User>(user);

            Unit.Users.Add(entity);
            Unit.Complete();

            var result = SetPassword(entity.Id, user.Password);
            Unit.Complete();

            return result;
        }

        public UserLoginModel GetForLoginByUserName(string userName)
        {
            return Mapper.Map<UserLoginModel>(Unit.Users.GetByUserNameWithPermissions(userName));
        }

        private bool SetPassword(long id, string password)
        {
            var user = Unit.Users.Get(id);

            if (user == null) return false;

            user.PasswordHash = Hasher.HashPassword(user, password);

            return true;
        }
    }
}
