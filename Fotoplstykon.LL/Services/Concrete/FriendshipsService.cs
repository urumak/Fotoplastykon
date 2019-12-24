using AutoMapper;
using Fotoplastykon.BLL.DTOs.Friendships;
using Fotoplastykon.BLL.DTOs.Notifications;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FriendshipsService : Service, IFriendshipsService
    {
        public FriendshipsService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<NotificationDTO> InviteFriend(long userId, long friendId)
        {
            await Unit.Invitations.Add(new Invitation
            {
                InvitingId = userId,
                InvitedId = friendId
            });

            var notification = await Unit.InvitationNotifications.Add(new InvitationNotification
            {
                FriendId = userId,
                UserId = friendId,
                Type = NotificationType.InvitationSent
            });

            await Unit.Complete();

            return Mapper.Map<NotificationDTO>(notification);
        }

        public async Task<NotificationDTO> AcceptInvitation(long userId, long invitedId)
        {
            var invitation = await Unit.Invitations.Get(userId, invitedId);

            await Unit.Friendships.Add(new Friendship
            {
                InvitingId = invitation.InvitingId,
                InvitedId = invitation.InvitedId
            });

            var notification = await Unit.InvitationNotifications.Add(new InvitationNotification
            {
                FriendId = userId,
                UserId = invitedId,
                Type = NotificationType.InvitationAccepted
            });

            Unit.Invitations.Remove(invitation);
            await Unit.Complete();

            return Mapper.Map<NotificationDTO>(notification);
        }

        public async Task RemoveFriend(long userId, long friendId)
        {
            var friendship = await Unit.Friendships.Get(f => f.InvitedId == userId && f.InvitingId == friendId);

            if (friendship == null) friendship = await Unit.Friendships.Get(f => f.InvitedId == friendId && f.InvitingId == userId);

            Unit.Friendships.Remove(friendship);
            await Unit.Complete();
        }

        public async Task<bool> CheckIfFriendshipExist(long firstId, long secondId)
        {
            return await Unit.Friendships.Get(firstId, secondId) != null;
        }

        public async Task<bool> CheckIfInvitationExist(long firstId, long secondId)
        {
            return await Unit.Invitations.Get(firstId, secondId) != null;
        }

        public async Task<bool> CheckIfInvitationExistByInvitationRoles(long invitedId, long invitingId)
        {
            return await Unit.Invitations.GetByInvitationRoles(invitedId, invitingId) != null;
        }

        public async Task<long> RemoveInvitation(long userId, long friendId)
        {
            var invitation = await Unit.Invitations.Get(userId, friendId);
            var notification = await Unit.InvitationNotifications.GetLast(userId, friendId);

            Unit.Invitations.Remove(invitation);
            Unit.InvitationNotifications.Remove(notification);

            await Unit.Complete();

            return notification.Id;
        }

        public async Task<IInfiniteScrollResult<LinkedItemDTO>> GetFriends(IInfiniteScroll scroll, long userId)
        {
            var data = await Unit.Users.GetListForInfiniteScroll(scroll, u => u.AcceptedFriends, q => q.FirstName, OrderDirection.ASC);
            return new InfiniteScrollResult<LinkedItemDTO>
            {
                Items = Mapper.Map<List<LinkedItemDTO>>(data.Items),
                Scroll = data.Scroll
            };
        }

        public async Task<List<InvitationDTO>> GetInvitations(long userId)
        {
            return Mapper.Map<List<InvitationDTO>>(await Unit.Invitations.GetInvitations(userId));
        }

    }
}
