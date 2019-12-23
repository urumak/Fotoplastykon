using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Friendships
{
    public class InvitationDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string PhotoUrl { get; set; }
        public string NameAndSurname { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
