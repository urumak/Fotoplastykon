using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Auth
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
