using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class ChatService : Service, IChatService
    {
        public ChatService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {

        }

        public async Task<IInfiniteScrollResult<Message>> GetMessages(IInfiniteScroll scroll, long userId, long friendId)
        {
            return await Unit.Messages.GetListForInfiniteScroll(
                scroll,
                m => (m.SenderId == userId && m.ReceiverId == friendId) || (m.ReceiverId == userId && m.SenderId == friendId),
                s => s.DateCreated, OrderDirection.DESC);
        }

        public async Task WriteMessage(long userId, Message message)
        {
            message.SenderId = userId;
            await Unit.Messages.Add(message);
            await Unit.Complete();
        }
    }
}
