using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class FriendshipsRepository : Repository<Friendship>, IFriendshipsRepository
    {
        public FriendshipsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<Friendship> Get(long firstId, long secondId)
        {
            var friendship = await DatabaseContext.Friendships
                .FirstOrDefaultAsync(f => f.InvitedId == firstId && f.InvitingId == secondId);

            if (friendship == null) friendship = await DatabaseContext.Friendships
                    .FirstOrDefaultAsync(f => f.InvitedId == secondId && f.InvitingId == firstId);

            return friendship;
        }

        public async Task<Friendship> GetByIvitingAndInvitedId(long invitingId, long invitedId)
        {
            return await DatabaseContext.Friendships
                .FirstOrDefaultAsync(f => f.InvitedId == invitedId && f.InvitingId == invitingId);
        }
    }
}
