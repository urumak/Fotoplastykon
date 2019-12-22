using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Messages
{
    public class LastMessage
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public bool Unread { get; set; }
        public string PhotoUrl { get; set; }
        public string MessageText { get; set; }
        public string NameAndSurname { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
