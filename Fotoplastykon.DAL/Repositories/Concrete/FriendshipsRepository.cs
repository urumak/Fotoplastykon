using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
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

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public Friendship Get(long firstId, long secondId)
        {
            var friendship = DatabaseContext.Friendships
                .FirstOrDefault(f => f.InvitedId == firstId && f.InvitingId == secondId);

            if (friendship == null) friendship = DatabaseContext.Friendships
                    .FirstOrDefault(f => f.InvitedId == secondId && f.InvitingId == firstId);

            return friendship;
        }

        public Friendship GetByIvitingAndInvitedId(long invitingId, long invitedId)
        {
            return DatabaseContext.Friendships
                .FirstOrDefault(f => f.InvitedId == invitedId && f.InvitingId == invitingId);
        }
    }
}
