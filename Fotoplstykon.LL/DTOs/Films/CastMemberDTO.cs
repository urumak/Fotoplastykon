using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class CastMemberDTO
    {
        public long PersonId { get; set; }
        public string FullName { get; set; }
        public string CharacterName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
