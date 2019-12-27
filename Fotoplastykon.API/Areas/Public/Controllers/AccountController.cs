using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService Account { get; }

        public AccountController(IAccountService account)
        {
            Account = account;
        }

        [HttpPost("change-profile-photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeProfilePhoto([FromForm]IFormFile file)
        {
            await Account.ChangeProfilePhoto(User.Id(), file);
            return Ok();
        }


        [HttpDelete("remove-profile-photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveProfilePhoto()
        {
            await Account.RomoveProfilePhoto(User.Id());
            return Ok();
        }

    }
}