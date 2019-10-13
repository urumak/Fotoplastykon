using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FriendshipsRepository : Repository<Friendship>, IFriendshipsRepository
    {
        public FriendshipsRepository(DatabaseContext context)
            : base(context)
        {
        }

        public Friendship Get(long firstId, long secondId)
        {
            var friendship = Context.Set<Friendship>()
                .FirstOrDefault(f => f.InvitedId == firstId && f.InvitingId == secondId);

            if (friendship == null) friendship = Context.Set<Friendship>()
                    .FirstOrDefault(f => f.InvitedId == secondId && f.InvitingId == firstId);

            return friendship;
        }

        public Friendship GetByIvitingAndInvitedId(long invitingId, long invitedId)
        {
            return Context.Set<Friendship>()
                .FirstOrDefault(f => f.InvitedId == invitedId && f.InvitingId == invitingId);
        }
    }
}
