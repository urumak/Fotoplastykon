using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class ConversationsRepository : Repository<Conversation>, IConversationsRepository
    {
        public ConversationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IInfiniteScrollResult<Conversation>> GetLastMessagesForEachFriend(IInfiniteScroll scroll, long userId)
        {
            return await DatabaseContext.Conversations
                .Include(c => c.LastMessage)
                .ThenInclude(m => m.Sender)
                .Include(c => c.LastMessage)
                .ThenInclude(m => m.Receiver)
                .Where(c => c.FirstUserId == userId || c.SecondUserId == userId)
                .OrderByDescending(c => c.LastMessage.DateCreated)
                .GetInfiniteScrollResult(scroll);
        }

        public async Task<Conversation> Get(long userId, long friendId)
        {
            return await DatabaseContext.Conversations
                .FirstOrDefaultAsync(c => (c.FirstUserId == userId && c.SecondUserId == friendId)
                    || (c.FirstUserId == friendId && c.SecondUserId == userId));
        }
    }
}
