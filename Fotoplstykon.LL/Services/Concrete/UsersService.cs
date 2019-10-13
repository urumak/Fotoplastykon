using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Fotoplastykon.BLL.Models.Users;
using System.Linq;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class UsersService : Service, IUsersService
    {
        public UsersService(IUnitOfWork unit, IMapper mapper, IPasswordHasher<User> hasher)
            : base(unit, mapper)
        {
            Hasher = hasher;
        }

        private IPasswordHasher<User> Hasher { get; }

        public User Get(long id)
        {
            return Unit.Users.Get(id);
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

        public List<User> Search(string searchString)
        {
            //TODO: poprawić wyszukiwanie tak, żeby dało się szukać po imieniu i nazwisku
            return Unit.Users
                .Find(u => u.UserName.Contains(searchString)
                    || u.FirstName.Contains(searchString)
                    || u.Surname.Contains(searchString)).ToList();
        }

        public bool CheckIfExists(long id)
        {
            return Unit.Users.Get(id) != null;
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
