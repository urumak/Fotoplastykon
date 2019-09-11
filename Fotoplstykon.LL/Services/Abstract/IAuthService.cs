using Fotoplastykon.BLL.Models.Auth;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IAuthService
    {
        LoginResult TryLoginUser(string userName, string password);
        TokenModel TryRefreshToken(long userId);
        TokenModel TryRecoverToken(string refreshToken);
        bool TryRevokeToken(string refreshToken);
    }
}
