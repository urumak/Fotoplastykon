using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.SignalR;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        public ISignalRService SignalRService { get; set; }
        public DatabaseContext DbContext { get; set; }

        public ChatHub(ISignalRService signalRService, DatabaseContext dbContext)
        {
            SignalRService = signalRService;
            DbContext = dbContext;
        }

        public override async Task OnConnectedAsync()
        {
            await SignalRService.Connect(new SignalRConnectionDTO
            {
                UserId = Context.User.Id(),
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                ConnectionId = Context.ConnectionId
            });

            //DbContext.SignalRConnections.Add(new DAL.Entities.Concrete.SignalRConnection
            //{
            //    UserId = Context.User.Id(),
            //    UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
            //    ConnectionId = Context.ConnectionId
            //});
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await SignalRService.Disconnect(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
