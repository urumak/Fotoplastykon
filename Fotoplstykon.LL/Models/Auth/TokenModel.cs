using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Models.Auth
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
