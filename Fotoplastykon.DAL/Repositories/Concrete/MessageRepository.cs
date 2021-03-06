﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class MessageRepository : Repository<Message>, IMessagesRepository
    {
        public MessageRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<IInfiniteScrollResult<Message>> GetListForInfiniteScroll(IInfiniteScroll scroll, long principalId, long friendId)
        {
            return await DatabaseContext.Messages
                .Where(m => (m.ReceiverId == principalId && m.SenderId == friendId)
                        || (m.ReceiverId == friendId && m.SenderId == principalId))
                .OrderByDescending(m => m.DateCreated)
                .GetInfiniteScrollResult(scroll);
        }

        public async Task<List<Message>> GetLastUnreadMessagesFromEachFriend(long receiverId)
        {
            return await DatabaseContext.Messages
                .Where(m => m.ReceiverId == receiverId && 
                    (m.Receiver.UnreadMessages.FirstOrDefault(x => x.SenderId == m.SenderId) == null 
                        || m.Receiver.UnreadMessages.FirstOrDefault(x => x.SenderId == m.SenderId).LastReadingDate == null 
                        || m.Receiver.UnreadMessages.FirstOrDefault(x => x.SenderId == m.SenderId).LastReadingDate < m.DateCreated))
                .GroupBy(m => m.SenderId)
                .OrderByDescending(m => m.Max(o => o.DateCreated))
                .Select(m => m.FirstOrDefault())
                .ToListAsync();
        }

        public async Task<IInfiniteScrollResult<Message>> GetLastMessagesForEachFriend(IInfiniteScroll scroll, long userId)
        {
            return await DatabaseContext.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.ReceiverId == userId || m.SenderId == userId)
                .GroupBy(m => m.SenderId)
                .OrderByDescending(m => m.Max(o => o.DateCreated))
                .Select(m => m.LastOrDefault())
                .GetInfiniteScrollResult(scroll);
        }
    }
}
