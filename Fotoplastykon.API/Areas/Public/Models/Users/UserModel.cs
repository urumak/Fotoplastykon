using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Users
{
    public class UserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
