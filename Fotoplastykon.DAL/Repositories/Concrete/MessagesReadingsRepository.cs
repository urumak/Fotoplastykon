using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class MessagesReadingsRepository : Repository<MessagesReading>, IMessagesReadingsRepository
    {
        public MessagesReadingsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<List<MessagesReading>> GetByReceiverId(long receiverId)
        {
            return await DatabaseContext.MessagesReadings.Where(r => r.ReceiverId == receiverId).ToListAsync();
        }

        public async Task<List<MessagesReading>> GetByReceiverAndSendersIds(long receiverId, List<long> sendersIds)
        {
            return await DatabaseContext.MessagesReadings
                .Where(r => r.ReceiverId == receiverId && sendersIds.Contains(r.SenderId))
                .ToListAsync();
        }
    }
}
