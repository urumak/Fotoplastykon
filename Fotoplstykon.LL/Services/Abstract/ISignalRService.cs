using Fotoplastykon.BLL.DTOs.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface ISignalRService
    {
        Task<List<string>> GetUserConnections(long userId);
        Task Connect(SignalRConnectionDTO connection);
        Task Disconnect(string connectionId);
    }
}
