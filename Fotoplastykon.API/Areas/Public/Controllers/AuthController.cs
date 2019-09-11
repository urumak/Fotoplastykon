using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService Users { get; }
        private IAuthService Auth { get; }
        private IMapper Mapper { get; }

        public AuthController(IUserService users, IAuthService auth, IMapper mapper)
        {
            Users = users;
            Auth = auth;
            Mapper = mapper;
        }

        #region Register()
        [HttpPost("register")]
        public IActionResult Register(RegisterModel user)
        {
            Users.Add(Mapper.Map<AddUserModel>(user));

            return Ok();
        }
        #endregion

        #region TryLoginUser()
        [HttpPost("login")]
        public ActionResult<TokenViewModel> Login([FromBody] LoginModel model)
        {
            var loginResult = Auth.TryLoginUser(model.UserName, model.Password);

            if (loginResult.CorrectCredentials) return Mapper.Map<TokenViewModel>(loginResult.Token);

            return BadRequest("Nazwa użytkownika lub hasło są nieprawidłowe.");
        }
        #endregion

        #region RefreshToken()
        [HttpGet("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<TokenViewModel> RefreshToken([FromHeader(Name = "Authorization")] string authorization)
        {
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var jwtValue = authorization.Replace("Bearer ", String.Empty);

                if (new JwtSecurityTokenHandler().ReadToken(jwtValue) is JwtSecurityToken jwt)
                {
                    var id = Convert.ToInt64(jwt.Subject);
                    var token = Mapper.Map<TokenViewModel>(Auth.TryRefreshToken(id));

                    if (token != null) return token;
                }
            }

            return BadRequest("Nie udało się odświeżyć tokena.");
        }
        #endregion

        #region RecoverToken()
        [HttpGet("recover-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<TokenViewModel> RecoverToken([FromBody]TokenRequestModel refreshToken)
        {
            if (!string.IsNullOrEmpty(refreshToken.Token))
            {
                var recoveredToken = Mapper.Map<TokenViewModel>(Auth.TryRecoverToken(refreshToken.Token));

                if(recoveredToken != null) return recoveredToken;
            }

            return BadRequest("Nie udało się odświeżyć tokena.");
        }
        #endregion

        #region
        [HttpPost("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult RevokeToken([FromBody]TokenRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Token)) return BadRequest();

            if (!Users.CheckIfExistByRefreshToken(model.Token)) return NotFound();

            if(Auth.TryRevokeToken(model.Token)) return Ok("Token został usunięty");

            return BadRequest("Nie udało się usunąć tokena");
        }
        #endregion
    }
}