
namespace Fotoplastykon.BLL.DTOs.Auth
{
    public class LoginResult
    {
        public bool CorrectCredentials { get; set; }
        public TokenDTO Token { get; set; }
    }
}
