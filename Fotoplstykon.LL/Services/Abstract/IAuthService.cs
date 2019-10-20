using Fotoplastykon.BLL.Models.Auth;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IAuthService
    {
        Task<LoginResult> TryLoginUser(string userName, string password);
        Task<TokenModel> TryRefreshToken(long userId);
    }
}
