using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Messages
{
    public class MessageDTO
    {
        public long Id { get; set; }
        public string MessageText { get; set; }
        public bool IsSender { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
