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
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.API.Areas.Public.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/friendships")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private IFriendshipsService Friendships { get; }
        private IUsersService Users { get; }
        private IHubContext<NotificationsHub, INotificationsHub> HubContext { get; }
        private ISignalRService SignalRService { get; }
        private INotificationsService Notifications { get; }

        public FriendshipsController(IFriendshipsService friendships, IUsersService users, IHubContext<NotificationsHub, INotificationsHub> hubContext, ISignalRService signalRService, INotificationsService notifications)
        {
            Friendships = friendships;
            Users = users;
            HubContext = hubContext;
            SignalRService = signalRService;
            Notifications = notifications;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserFriends([FromQuery]InfiniteScroll scroll)
        {
            return Ok(await Friendships.GetFriends(scroll, User.Id()));
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

            await HubContext.Clients.Clients(await SignalRService.GetUserConnections(model.FriendId))
                .NotificationReceived(await Friendships.InviteFriend(User.Id(), model.FriendId));

            return Ok();
        }

        [HttpPost("accept-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AcceptInvitation([FromBody]FriendIdModel model)
        {
            if(!await Friendships.CheckIfInvitationExistByInvitationRoles(User.Id(), model.FriendId)) return NotFound();

            await HubContext.Clients.Clients(await SignalRService.GetUserConnections(model.FriendId))
                .NotificationReceived(await Friendships.AcceptInvitation(User.Id(), model.FriendId));

            await Notifications.SetDecisionAccepted(User.Id(), model.FriendId);

            await HubContext.Clients.Clients
                ((await SignalRService.GetUserConnections(model.FriendId))
                    .Concat(await SignalRService.GetUserConnections(User.Id())).ToList())
                .RefreshChatList();

            return Ok();
        }

        [HttpDelete("refuse-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RefuseInvitation([FromBody]FriendIdModel model)
        {
            if (!await Friendships.CheckIfInvitationExistByInvitationRoles(User.Id(), model.FriendId)) return NotFound();
            await Friendships.RemoveInvitation(User.Id(), model.FriendId);
            await Notifications.SetDecisionRefused(User.Id(), model.FriendId);

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

            await HubContext.Clients.Clients
                ((await SignalRService.GetUserConnections(model.FriendId))
                    .Concat(await SignalRService.GetUserConnections(User.Id())).ToList())
                .RefreshChatList();

            return Ok();
        }

        [HttpDelete("cancel-invitation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CancelInvitation([FromBody]FriendIdModel model)
        {
            if (!await Users.CheckIfExists(model.FriendId)) return NotFound();
            if (!await Friendships.CheckIfInvitationExistByInvitationRoles(model.FriendId, User.Id())) return NotFound();

            await HubContext.Clients.Clients(await SignalRService.GetUserConnections(model.FriendId))
                .RefreshNotifications(await Friendships.RemoveInvitation(User.Id(), model.FriendId));

            return Ok();
        }
    }
}