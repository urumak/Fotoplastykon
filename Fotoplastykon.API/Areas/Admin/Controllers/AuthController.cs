using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService Users { get; }

        public AuthController(IUserService users)
        {
            Users = users;
        }

        //#region TryLoginUser()
        //[HttpPost("login")]
        //public Task<ActionResult<TokenViewModel>> TryLoginUser([FromBody] LoginFormModel model)
        //{
        //    var user = Users.GetUserByUsername(model.Username);

        //    if (user != null && user.Enabled)
        //    {
        //        var result = Users.VerifyPassword(user, model.Password);
        //        var sessionId = AuthLog.CreateSession(user, result, tokenExpires);

        //        if (result == LoginResult.Success)
        //        {
        //            return CreateToken(user, sessionId, tokenExpires);
        //        }
        //    }

        //    return BadRequest("[[[Email lub hasło są nieprawidłowe.]]]");
        //}
        //#endregion
    }
}