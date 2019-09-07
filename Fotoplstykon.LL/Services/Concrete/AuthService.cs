using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.LL.Models.Auth;
using Fotoplastykon.LL.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Fotoplastykon.LL.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork Unit { get; }
        private IPasswordHasher<User> Hasher { get; }
        private Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        private User _user;

        public AuthService(IUnitOfWork unit, IPasswordHasher<User> hasher, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Unit = unit;
            Hasher = hasher;
            Configuration = configuration;
        }

        public LoginResult TryLoginUser(string userName, string password)
        {
            var result = new LoginResult();

            if (_user != null) return result;

            if (!CheckPassword(password)) return result;

            result.CorrectCredentials = true;
            result.Token = CreateToken();

            return result;
        }

        private bool FindUser(string userName)
        {
            _user = Unit.Users.GetByUserName(userName);

            return _user != null;
        }

        private bool CheckPassword(string password)
        {
            var result = Hasher.VerifyHashedPassword(_user, _user.PasswordHash, password);

            return result == PasswordVerificationResult.Success;
        }

        private string CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = CreateClaims();

            var token = new JwtSecurityToken(
                Configuration["Tokens:Issuer"],
                Configuration["Tokens:Issuer"],
                claims,
                signingCredentials: credentials
            );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        private IEnumerable<Claim> CreateClaims()
        {
            var canEditPagesWithIds = GetPagesIdsThatUserCanEdit();

            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("CanEditPages", canEditPagesWithIds)
            };
        }

        private string GetPagesIdsThatUserCanEdit()
        {
            var pagesIds = Unit.Creations.GetPagesIdsForUser(_user.Id);

            return JsonConvert.SerializeObject(pagesIds);
        }
    }
}
