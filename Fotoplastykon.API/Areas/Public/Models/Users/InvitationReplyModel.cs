using Fotoplastykon.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Users
{
    public class InvitationReplyModel
    {
        public long InvingId { get; set; }
        public InvitationReply Reply { get; set; }
    }
}
