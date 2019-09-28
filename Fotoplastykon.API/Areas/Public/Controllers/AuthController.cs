using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
        [AllowAnonymous]
        public IActionResult Register(RegisterModel user)
        {
            Users.Add(Mapper.Map<AddUserModel>(user));

            return Ok();
        }
        #endregion

        #region TryLoginUser()
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<TokenViewModel> Login([FromBody] LoginModel model)
        {
            var loginResult = Auth.TryLoginUser(model.UserName, model.Password);

            if (loginResult.CorrectCredentials) return Mapper.Map<TokenViewModel>(loginResult.Token);

            return BadRequest("Nazwa użytkownika lub hasło są nieprawidłowe.");
        }
        #endregion

        #region RefreshToken()
        [AllowAnonymous]
        [HttpGet("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult<TokenViewModel> RefreshToken([FromHeader(Name = "Authorization")] string authorization)
        {
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var user = User.Id();
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

        #region GetUser()
        [Route("user")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser()
        {
            var user = Users.Get(User.Id());

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion
    }
}