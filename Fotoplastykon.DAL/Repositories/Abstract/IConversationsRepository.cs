using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IConversationsRepository : IRepository<Conversation>
    {
        Task<IInfiniteScrollResult<Conversation>> GetLastMessagesForEachFriend(IInfiniteScroll scroll, long userId);
        Task<Conversation> Get(long userId, long friendId);
    }
}
