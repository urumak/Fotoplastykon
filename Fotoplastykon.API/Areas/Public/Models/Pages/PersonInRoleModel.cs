using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Pages
{
    public class PersonInRoleModel
    {
        public LinkedItemModel LinkedItem { get; set; }
        public string Role { get; set; }
        public string CharacterName { get; set; }
    }
}
