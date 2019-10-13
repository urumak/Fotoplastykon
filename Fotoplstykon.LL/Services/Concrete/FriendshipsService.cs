using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FriendshipsService : Service, IFriendshipsService
    {
        public FriendshipsService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
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

            if (friendship == null) friendship = Unit.Friendships.Get(f => f.InvitedId == friendId && f.InvitingId == userId);

            Unit.Friendships.Remove(friendship);
            Unit.Complete();
        }
    }
}
