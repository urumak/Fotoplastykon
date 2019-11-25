using AutoMapper;
using Fotoplastykon.BLL.DTOs.SignalR;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class SignalRService : Service, ISignalRService
    {
        public SignalRService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {

        }
        public async Task<List<string>> GetUserConnections(long userId)
        {
            return await Unit.SignalRConnections.GetUserConnectionsIds(userId);
        }

        public async Task Connect(SignalRConnectionDTO connection)
        {
            await Unit.SignalRConnections.Add(Mapper.Map<SignalRConnection>(connection));
            await Unit.Complete();
        }

        public async Task Disconnect(string connectionId)
        {
            var connection = await Unit.SignalRConnections.GetByConnectionId(connectionId);
            Unit.SignalRConnections.Remove(connection);
            await Unit.Complete();
        }
    }
}
