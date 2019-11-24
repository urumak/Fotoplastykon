using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendChatMessage(string message)
        {
            await Clients.All.ChatMessageReceived(Context.UserIdentifier, message);
        }
    }
}
