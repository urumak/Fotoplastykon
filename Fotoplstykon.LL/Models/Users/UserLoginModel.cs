using System.Collections.Generic;

namespace Fotoplastykon.LL.Models.Users
{
    public class UserLoginModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public List<long> PagesIds { get; set; }
    }
}
