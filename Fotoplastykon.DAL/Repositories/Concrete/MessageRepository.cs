using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;
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
    }
}
