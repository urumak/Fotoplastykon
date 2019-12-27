﻿using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Auth;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/auth")]
    [Authorize(Policy = "AdminAccess")]
    public class AuthController : ControllerBase
    {
        private IUsersService Users { get; }
        private IAuthService Auth { get; }
        private IMapper Mapper { get; }

        public AuthController(IUsersService users, IAuthService auth, IMapper mapper)
        {
            Users = users;
            Auth = auth;
            Mapper = mapper;
        }

        #region TryLoginUser()
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await Auth.TryLoginAdmin(model.UserName, model.Password);

            if (loginResult.CorrectCredentials) return Ok(Mapper.Map<TokenViewModel>(loginResult.Token));

            return BadRequest("Nazwa użytkownika lub hasło są nieprawidłowe.");
        }
        #endregion

        #region RefreshToken()
        [AllowAnonymous]
        [HttpGet("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RefreshToken([FromHeader(Name = "Authorization")] string authorization)
        {
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var user = User.Id();
                var jwtValue = authorization.Replace("Bearer ", string.Empty);

                if (new JwtSecurityTokenHandler().ReadToken(jwtValue) is JwtSecurityToken jwt)
                {
                    var id = Convert.ToInt64(jwt.Subject);
                    var token = Mapper.Map<TokenViewModel>(await Auth.TryRefreshToken(id));

                    if (token != null) return Ok(token);
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
        public async Task<IActionResult> GetUser()
        {
            var user = await Users.Get(User.Id());

            if (user == null) return NotFound();

            return Ok(Mapper.Map<UserProfileModel>(user));
        }
        #endregion

        #region Anonymise()
        [Route("anonymise")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Anonymise()
        {
            await Users.Anonymise(User.Id());
            return Ok();
        }
        #endregion
    }
}