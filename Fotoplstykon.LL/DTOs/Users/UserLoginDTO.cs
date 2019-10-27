using System.Collections.Generic;

namespace Fotoplastykon.BLL.DTOs.Users
{
    public class UserLoginDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
