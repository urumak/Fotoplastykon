using Fotoplastykon.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Films
{
    public class PersonInRoleFormModel
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public RoleType Role { get; set; }
        public string CharacterName { get; set; }
    }
}
