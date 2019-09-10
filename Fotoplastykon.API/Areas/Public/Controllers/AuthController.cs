using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.BLL.Models.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

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

        #region TryLoginUser()
        [HttpPost("login")]
        public ActionResult<TokenViewModel> Login([FromBody] LoginModel model)
        {
            var loginResult = Auth.TryLoginUser(model.UserName, model.Password);

            if (loginResult.CorrectCredentials)
            {
                return Mapper.Map<TokenViewModel>(loginResult.Token);
            }

            return BadRequest("Nazwa użytkownika lub hasło są nieprawidłowe");
        }
        #endregion

        #region Register()
        [Route("register")]
        public IActionResult Register(RegisterModel user)
        {
            Users.Add(Mapper.Map<AddUserModel>(user));

            return Ok();
        }
        #endregion
    }
}