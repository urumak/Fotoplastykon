using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IMessagesRepository : IRepository<Message>
    {
        Task<IInfiniteScrollResult<Message>> GetListForInfiniteScroll(IInfiniteScroll scroll, long principalId, long friendId);
        Task<List<Message>> GetLastUnreadMessagesFromEachFriend(long receiverId);
        Task<IInfiniteScrollResult<Message>> GetLastMessagesForEachFriend(IInfiniteScroll scroll, long receiverId);
    }
}
