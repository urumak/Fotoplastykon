namespace Fotoplastykon.LL.Models
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
