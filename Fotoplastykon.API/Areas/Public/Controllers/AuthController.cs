using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DateTime tokenExpires = DateTime.Now.AddMinutes(30);
        private IUserService Users { get; }
        private IAuthService Auth { get; }

        public AuthController(IUserService users, IAuthService auth)
        {
            Users = users;
            Auth = auth;
        }

        #region TryLoginUser()
        [HttpPost("login")]
        public ActionResult<TokenModel> Login([FromBody] LoginModel model)
        {
            var user = Users.GetForLoginByUserName(model.UserName);

            if (user != null)
            {
            //    var result = Users.VerifyPassword(user, model.Password);

            //    if (result == LoginResult.Success)
            //    {
            //        return CreateToken(user, tokenExpires);
            //    }
            }

            return BadRequest("[[[Email lub hasło są nieprawidłowe.]]]");
        }
        #endregion
    }
}