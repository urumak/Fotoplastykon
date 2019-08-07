using Fotoplastykon.LL.Models.Auth;

namespace Fotoplastykon.LL.Services.Abstract
{
    public interface IAuthService
    {
        LoginResult TryLoginUser(string userName, string password);
    }
}
