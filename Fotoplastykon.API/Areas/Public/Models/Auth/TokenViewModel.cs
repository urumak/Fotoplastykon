using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Auth
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
