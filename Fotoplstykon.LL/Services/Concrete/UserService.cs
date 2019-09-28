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
    public class UserService : IUserService
    {
        public UserService(IUnitOfWork unit, IMapper mapper, IPasswordHasher<User> hasher)
        {
            Unit = unit;
            Mapper = mapper;
            Hasher = hasher;
        }

        private IUnitOfWork Unit { get; }
        private IMapper Mapper { get; }
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

        private bool SetPassword(long id, string password)
        {
            var user = Unit.Users.Get(id);

            if (user == null) return false;

            user.PasswordHash = Hasher.HashPassword(user, password);

            return true;
        }

        public void InviteFriend(long userId, long friendId)
        {
            var invitation = new Invitation
            {
                InvitingId = userId,
                InvitedId = friendId
            };

            Unit.Invitations.Add(invitation);
            Unit.Complete();
        }

        public void AcceptInvitation(long userId, long invitedId)
        {
            var invitation = Unit.Invitations.Get(u => u.InvitedId == invitedId && u.InvitingId == userId);

            var friendship = new Friendship
            {
                InvitingId = invitation.InvitingId,
                InvitedId = invitation.InvitedId
            };

            Unit.Friendships.Add(friendship);
            Unit.Invitations.Remove(invitation);
            Unit.Complete();
        }

        public void RefuseInvitation(long userId, long invitedId)
        {
            var invitation = Unit.Invitations.Get(u => u.InvitedId == invitedId && u.InvitingId == userId);
            Unit.Invitations.Remove(invitation);
            Unit.Complete();
        }

        public void RemoveFriend(long userId, long friendId)
        {
            var friendship = Unit.Friendships.Get(f => f.InvitedId == userId && f.InvitingId == friendId);

            if(friendship == null) Unit.Friendships.Get(f => f.InvitedId == friendId && f.InvitingId == userId);

            Unit.Friendships.Remove(friendship);
            Unit.Complete();
        }
    }
}
