using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Friendships;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/friendships")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private IFriendshipsService Friendships { get; }
        private IUsersService Users { get; }

        public FriendshipsController(IFriendshipsService friendships, IUsersService users)
        {
            Friendships = friendships;
            Users = users;
        }

        [HttpPost("invite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Invite([FromBody]FriendIdModel model)
        {
            if (model.FriendId == User.Id()) return BadRequest("Nie można wysłać zaproszenia do siebie");
            if (!await Users.CheckIfExists(model.FriendId)) return NotFound();
            if (await Friendships.CheckIfFriendshipExist(User.Id(), model.FriendId)) return BadRequest("Użytkownicy są już znajomymi.");
            await Friendships.InviteFriend(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpPost("accept-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AcceptInvitation([FromBody]FriendIdModel model)
        {
            if(!await Friendships.CheckIfInvitationExist(User.Id(), model.FriendId)) return NotFound();
            await Friendships.AcceptInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpPost("refuse-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RefuseInvitation([FromBody]FriendIdModel model)
        {
            if (!await Friendships.CheckIfInvitationExist(User.Id(), model.FriendId)) return NotFound();
            await Friendships.RefuseInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpDelete("remove-friend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveFriend([FromBody]FriendIdModel model)
        {
            if (!await Users.CheckIfExists(model.FriendId)) return NotFound();
            if (!await Friendships.CheckIfFriendshipExist(User.Id(), model.FriendId)) return NotFound();

            await Friendships.RemoveFriend(User.Id(), model.FriendId);
            return Ok();
        }
    }
}