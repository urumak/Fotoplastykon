using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.SignalR
{
    public class SignalRConnectionDTO
    {
        public long UserId { get; set; }
        public string UserAgent { get; set; }
        public string ConnectionId { get; set; }
    }
}
