using Fotoplastykon.BLL.DTOs.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Hubs
{
    public interface IChatHub
    {
        Task ChatMessageReceived(long senderId, MessageDTO message);
    }
}
