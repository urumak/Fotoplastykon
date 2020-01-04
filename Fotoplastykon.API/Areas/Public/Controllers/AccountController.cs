using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper Mapper { get; }
        private IAccountService Account { get; }
        private IUsersService Users { get; }

        public AccountController(IAccountService account, IUsersService users, IMapper mapper)
        {
            Account = account;
            Users = users;
            Mapper = mapper;
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

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody]UserFormModel model)
        {
            await Users.Update(User.Id(), Mapper.Map<AddUserDTO>(model), model.Password);
            return Ok();
        }
    }
}