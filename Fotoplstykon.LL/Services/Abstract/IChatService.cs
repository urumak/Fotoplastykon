using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IChatService
    {
        Task<IInfiniteScrollResult<Message>> GetMessages(IInfiniteScroll scroll, long userId, long friendId);
        Task WriteMessage(long userId, Message message);
    }
}
