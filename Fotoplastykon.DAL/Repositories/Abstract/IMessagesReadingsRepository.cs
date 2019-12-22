using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IMessagesReadingsRepository : IRepository<MessagesReading>
    {
        Task<List<MessagesReading>> GetByReceiverId(long receiverId);
        Task<List<MessagesReading>> GetByReceiverAndSendersIds(long receiverId, List<long> sendersIds);
    }
}
