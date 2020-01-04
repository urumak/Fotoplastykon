using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Admin.Models.Users;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/users")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class UsersController : ControllerBase
    {
        protected IUsersService Users { get; }
        protected IMapper Mapper { get; }
        protected IAccountService Account { get; }

        public UsersController(IUsersService users, IMapper mapper, IAccountService account)
        {
            Users = users;
            Mapper = mapper;
            Account = account;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList([FromQuery]Pager pager)
        {
            return Ok(await Users.GetList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Fetch(long id)
        {
            var user = await Users.Get(id);
            if (user == null) return NotFound();

            return Ok(Mapper.Map<UserFormModel>(user));
        }


        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, [FromBody]UserFormModel model)
        {
            if (!await Users.CheckIfExists(id)) return NotFound();
            var newPassword = !string.IsNullOrEmpty(model.Password) ? model.Password : null;
            await Users.Update(id, Mapper.Map<AddUserDTO>(model), newPassword);

            return Ok();
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Add([FromBody]UserFormModel model)
        {
            return Ok(await Users.Add(Mapper.Map<AddUserDTO>(model), model.IsAdmin));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await Users.CheckIfExists(id)) return NotFound();

            await Users.Anonymise(id);

            return Ok();
        }

        [HttpPost("change-profile-photo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeProfilePhoto(long id, [FromForm]IFormFile file)
        {
            await Account.ChangeProfilePhoto(id, file);
            return Ok();
        }
    }
}