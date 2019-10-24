using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fotoplastykon.BLL.DTOs.Auth;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class AuthService : IAuthService
    {
        public AuthService(IUnitOfWork unit, IPasswordHasher<User> hasher, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Unit = unit;
            Hasher = hasher;
            Configuration = configuration;
        }

        private IUnitOfWork Unit { get; }
        private IPasswordHasher<User> Hasher { get; }
        private Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        private readonly DateTime tokenExpirationDate = DateTime.Now.AddMinutes(30);
        private User _user;

        public async Task<LoginResult> TryLoginUser(string userName, string password)
        {
            var result = new LoginResult();

            if (!await FindUser(userName)) return result;

            if (!CheckPassword(password)) return result;

            result.CorrectCredentials = true;
            result.Token = await CreateToken();

            return result;
        }

        public async Task<TokenDTO> TryRefreshToken(long userId)
        {
            if (!await FindUser(userId)) return null;

            return await CreateToken();
        }

        private async Task<bool> FindUser(string userName)
        {
            _user = await Unit.Users.GetByUserName(userName);

            return _user != null;
        }

        private async Task<bool> FindUser(long userId)
        {
            _user = await Unit.Users.Get(userId);

            return _user != null;
        }

        private bool CheckPassword(string password)
        {
            var result = Hasher.VerifyHashedPassword(_user, _user.PasswordHash, password);

            return result == PasswordVerificationResult.Success;
        }

        private async Task<TokenDTO> CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = await CreateClaims();

            var token = new JwtSecurityToken(
                Configuration["Tokens:Issuer"],
                Configuration["Tokens:Issuer"],
                claims,
                expires: tokenExpirationDate,
                signingCredentials: creds
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDTO()
            {
                Token = tokenValue,
                ExpirationDate = token.ValidTo
            };
        }

        private async Task<IEnumerable<Claim>> CreateClaims()
        {
            var canEditPagesWithIds = await GetPagesIdsThatUserCanEdit();

            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(ClaimTypes.Name, _user.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("CanEditPages", canEditPagesWithIds),
                new Claim("IsAdmin", _user.IsAdmin.ToString())
            };
        }

        private async Task<string> GetPagesIdsThatUserCanEdit()
        {
            var filmPagesIds = await Unit.FilmPagesCreations.GetPagesIdsForUser(_user.Id);
            var personPagesIds = await Unit.PersonPagesCreations.GetPagesIdsForUser(_user.Id);

            return JsonConvert.SerializeObject(filmPagesIds.Concat(personPagesIds));
        }
    }
}
