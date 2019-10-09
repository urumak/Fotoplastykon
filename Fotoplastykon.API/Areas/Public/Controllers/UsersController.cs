using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Areas.Public.Models.Users;
using Fotoplastykon.API.Enums;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService Users { get; }
        private IMapper Mapper { get; }

        public UsersController(IUserService users, IMapper mapper)
        {
            Users = users;
            Mapper = mapper;
        }

        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IEnumerable<ListItemModel> Search(string search)
        {
            return Mapper.Map<List<ListItemModel>>(Users.Search(search));
        }

        [HttpPost("invite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Invite([FromBody]FriendIdModel model)
        {
            Users.InviteFriend(User.Id(), model.FriendId);
            return Ok();
        }

        [HttpPost("accept-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult AcceptInvitation([FromBody]FriendIdModel model)
        {
            Users.AcceptInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpPost("refuse-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RefuseInvitation([FromBody]FriendIdModel model)
        {
            Users.RefuseInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpDelete("remove-friend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RemoveFriend([FromBody]FriendIdModel model)
        {
            Users.RemoveFriend(User.Id(), model.FriendId);
            return Ok();
        }
    }
}