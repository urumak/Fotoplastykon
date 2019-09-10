
namespace Fotoplastykon.BLL.Models.Auth
{
    public class LoginResult
    {
        public bool CorrectCredentials { get; set; }
        public TokenModel Token { get; set; }
    }
}
