namespace Fotoplastykon.LL.Models.Users
{
    public class AddUserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
