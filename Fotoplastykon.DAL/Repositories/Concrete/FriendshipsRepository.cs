using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IInfiniteScrollResult<User>> GetListForInfiniteScroll(IInfiniteScroll scroll, long userId)
        {
            return await DatabaseContext.Friendships
                .Where(f => f.InvitedId == userId).Select(u => u.Inviting)
                .Union(DatabaseContext.Friendships.Where(f => f.InvitingId == userId).Select(u => u.Invited))
                .OrderBy(u => u.FirstName)
                .GetInfiniteScrollResult(scroll);
        }

        public async Task<List<User>> SearchForFriends(string searchInput, long userId, int limit = 20)
        {
            var friends = await DatabaseContext.Friendships
                .Where(f => f.InvitedId == userId && (f.Inviting.FirstName.StartsWith(searchInput) || f.Inviting.Surname.StartsWith(searchInput))).Select(u => u.Inviting)
                .Union(DatabaseContext.Friendships.Where(f => f.InvitingId == userId && (f.Invited.FirstName.StartsWith(searchInput) || f.Invited.Surname.StartsWith(searchInput))).Select(u => u.Invited))
                .OrderBy(u => u.FirstName).Take(limit).ToListAsync();

            if (friends == null || friends.Count == 0)
            {
                friends = await DatabaseContext.Friendships
                    .Where(f => f.InvitedId == userId && (f.Inviting.FirstName.Contains(searchInput) || f.Inviting.Surname.Contains(searchInput))).Select(u => u.Inviting)
                    .Union(DatabaseContext.Friendships.Where(f => f.InvitingId == userId && (f.Invited.FirstName.Contains(searchInput) || f.Invited.Surname.Contains(searchInput))).Select(u => u.Invited))
                    .OrderBy(u => u.FirstName).Take(limit).ToListAsync();
            }

            return friends;
        }
    }
}
