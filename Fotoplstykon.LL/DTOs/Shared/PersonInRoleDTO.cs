using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Shared
{
    public class PersonInRoleDTO
    {
        public LinkedItemDTO LinkedItem { get; set; }
        public string Role { get; set; }
        public string CharacterName { get; set; }
    }
}
