using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IChatService
    {
        Task<IPaginationResult<Message>> GetMessages(IPager pager, long userId, long friendId);
        Task WriteMessage(long userId, Message message);
    }
}
