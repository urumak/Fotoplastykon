using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
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

        public async Task InviteFriend(long userId, long friendId)
        {
            var invitation = new Invitation
            {
                InvitingId = userId,
                InvitedId = friendId
            };

            await Unit.Invitations.Add(invitation);
            await Unit.Complete();
        }

        public async Task AcceptInvitation(long userId, long invitedId)
        {
            var invitation = await Unit.Invitations.Get(u => u.InvitedId == invitedId && u.InvitingId == userId);

            var friendship = new Friendship
            {
                InvitingId = invitation.InvitingId,
                InvitedId = invitation.InvitedId
            };

            await Unit.Friendships.Add(friendship);
            Unit.Invitations.Remove(invitation);
            await Unit.Complete();
        }

        public async Task RefuseInvitation(long userId, long invitedId)
        {
            var invitation = await Unit.Invitations.Get(u => u.InvitedId == invitedId && u.InvitingId == userId);
            Unit.Invitations.Remove(invitation);
            await Unit.Complete();
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
    }
}
