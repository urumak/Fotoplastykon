using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.SignalR;
using Fotoplastykon.BLL.Services.Abstract;
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

        public ChatHub(ISignalRService signalRService)
        {
            SignalRService = signalRService;
        }

        public override Task OnConnectedAsync()
        {
            SignalRService.Connect(new SignalRConnectionDTO
            {
                UserId = Context.User.Id(),
                UserAgent = Context.GetHttpContext().Request.Headers["User-Agent"],
                ConnectionId = Context.ConnectionId
            });
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            SignalRService.Disconnect(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
