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
        public IActionResult Invite([FromBody]FriendIdModel model)
        {
            if (!Users.CheckIfExists(model.FriendId)) return NotFound();
            if (Friendships.CheckIfFriendshipExist(User.Id(), model.FriendId)) return BadRequest("Użytkownicy są już znajomymi.");
            Friendships.InviteFriend(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpPost("accept-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult AcceptInvitation([FromBody]FriendIdModel model)
        {
            if(!Friendships.CheckIfInvitationExist(User.Id(), model.FriendId)) return NotFound();
            Friendships.AcceptInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpPost("refuse-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RefuseInvitation([FromBody]FriendIdModel model)
        {
            if (!Friendships.CheckIfInvitationExist(User.Id(), model.FriendId)) return NotFound();
            Friendships.RefuseInvitation(User.Id(), model.FriendId);

            return Ok();
        }

        [HttpDelete("remove-friend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RemoveFriend([FromBody]FriendIdModel model)
        {
            if (!Users.CheckIfExists(model.FriendId)) return NotFound();
            if (!Friendships.CheckIfFriendshipExist(User.Id(), model.FriendId)) return NotFound();

            Friendships.RemoveFriend(User.Id(), model.FriendId);
            return Ok();
        }
    }
}