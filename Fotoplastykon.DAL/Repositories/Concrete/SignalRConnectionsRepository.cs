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
    public class SignalRConnectionsRepository : Repository<SignalRConnection>, ISignalRConnectionsRepository
    {
        public SignalRConnectionsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<SignalRConnection> GetByConnectionId(string connectionId)
        {
            return await DatabaseContext.SignalRConnections.FirstOrDefaultAsync(c => c.ConnectionId == connectionId && c.DateDeleted == null);
        }

        public async Task<List<string>> GetUserConnectionsIds(long userId)
        {
            return await DatabaseContext.SignalRConnections.Where(c => c.UserId == userId && c.DateDeleted == null).Select(c => c.ConnectionId).ToListAsync();
        }
        public async Task<List<SignalRConnection>> GetUserConnections(long userId)
        {
            return await DatabaseContext.SignalRConnections.Where(c => c.UserId == userId && c.DateDeleted == null).ToListAsync();
        }
    }
}
