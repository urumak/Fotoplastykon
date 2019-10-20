using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFriendshipsService
    {
        Task InviteFriend(long userId, long friendId);
        Task AcceptInvitation(long userId, long invitingId);
        Task RefuseInvitation(long userId, long invitingId);
        Task RemoveFriend(long userId, long friendId);
        Task<bool> CheckIfFriendshipExist(long firstId, long secondId);
        Task<bool> CheckIfInvitationExist(long firstId, long secondId);
    }
}
