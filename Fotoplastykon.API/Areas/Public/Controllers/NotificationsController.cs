using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Hubs;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        protected INotificationsService Notifications { get; }
        protected IMapper Mapper { get; }
        protected IHubContext<NotificationsHub, INotificationsHub> HubContext { get; }
        private ISignalRService SignalRService { get; set; }

        public NotificationsController(INotificationsService notifications, IMapper mapper, IUsersService users, IFriendshipsService friendships, ISignalRService signalRService, IHubContext<NotificationsHub, INotificationsHub> hubContext)
        {
            Mapper = mapper;
            HubContext = hubContext;
            SignalRService = signalRService;
            Notifications = notifications;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNotifications([FromQuery]InfiniteScroll scroll)
        {
            return Ok(await Notifications.GetNotifications(scroll, User.Id()));
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetNotificationsCount()
        {
            return Ok(await Notifications.GetNotificationsCount(User.Id()));
        }


        [HttpPost("read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ReadMessages()
        {
            await Notifications.ReadMessages(User.Id());
            return Ok();
        }
    }
}