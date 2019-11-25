using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface ISignalRConnectionsRepository : IRepository<SignalRConnection>
    {
        Task<SignalRConnection> GetByConnectionId(string connectionId);
        Task<List<string>> GetUserConnectionsIds(long userId);
        Task<List<SignalRConnection>> GetUserConnections(long userId);
    }
}
