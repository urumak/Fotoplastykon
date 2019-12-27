using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IAccountService
    {
        Task ChangeProfilePhoto(long userId, IFormFile file);
        Task RomoveProfilePhoto(long userId);
    }
}
