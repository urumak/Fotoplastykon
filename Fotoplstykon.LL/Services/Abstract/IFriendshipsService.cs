using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFriendshipsService
    {
        void InviteFriend(long userId, long friendId);
        void AcceptInvitation(long userId, long invitingId);
        void RefuseInvitation(long userId, long invitingId);
        void RemoveFriend(long userId, long friendId);
        bool CheckIfFriendshipExist(long firstId, long secondId);
        bool CheckIfInvitationExist(long firstId, long secondId);
    }
}
