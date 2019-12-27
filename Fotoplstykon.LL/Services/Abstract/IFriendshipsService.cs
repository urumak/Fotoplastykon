using Fotoplastykon.BLL.DTOs.Notifications;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFriendshipsService
    {
        Task<NotificationDTO> InviteFriend(long userId, long friendId);
        Task<NotificationDTO> AcceptInvitation(long userId, long invitingId);
        Task RemoveFriend(long userId, long friendId);
        Task<long> RemoveInvitation(long userId, long friendId);
        Task<bool> CheckIfFriendshipExist(long firstId, long secondId);
        Task<bool> CheckIfInvitationExist(long firstId, long secondId);
        Task<bool> CheckIfInvitationExistByInvitationRoles(long invitedId, long invitingId);
        Task<IInfiniteScrollResult<LinkedItemDTO>> GetFriends(IInfiniteScroll scroll, long userId);
        Task<IPaginationResult<FriendListItemDTO>> GetPaginatedList(IPager pager, long userId);
    }
}
