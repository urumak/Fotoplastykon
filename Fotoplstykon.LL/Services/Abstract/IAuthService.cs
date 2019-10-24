using Fotoplastykon.BLL.DTOs.Auth;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IAuthService
    {
        Task<LoginResult> TryLoginUser(string userName, string password);
        Task<TokenDTO> TryRefreshToken(long userId);
    }
}
