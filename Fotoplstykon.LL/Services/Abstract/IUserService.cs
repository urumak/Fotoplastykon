using System.Collections.Generic;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IUserService
    {
        User Get(long id);
        bool Add(AddUserModel user);
        UserLoginModel GetForLoginByUserName(string userName);
        List<User> Search(string searchString);
        void InviteFriend(long userId, long friendId);
        void AcceptInvitation(long userId, long invitingId);
        void RefuseInvitation(long userId, long invitingId);
        void RemoveFriend(long userId, long friendId);
    }
}
