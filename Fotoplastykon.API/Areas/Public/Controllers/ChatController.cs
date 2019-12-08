using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Hubs;
using Fotoplastykon.API.Areas.Public.Models.Chat;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        protected IChatService Chat { get; }
        protected IMapper Mapper { get; }
        protected IUsersService Users { get; }
        protected IFriendshipsService Friendships { get; }
        protected IHubContext<ChatHub, IChatHub> HubContext { get; }
        private ISignalRService SignalRService { get; set; }

        public ChatController(IChatService chat, IMapper mapper, IUsersService users, IFriendshipsService friendships, ISignalRService signalRService, IHubContext<ChatHub, IChatHub> hubContext)
        {
            Chat = chat;
            Mapper = mapper;
            Users = users;
            Friendships = friendships;
            HubContext = hubContext;
            SignalRService = signalRService;
        }

        [HttpGet("{friendId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetMessages(InfiniteScroll scroll, long friendId)
        {
            if (!await Friendships.CheckIfFriendshipExist(User.Id(), friendId)) return NotFound();
            var result = await Chat.GetMessages(scroll, User.Id(), friendId);

            return Ok(Mapper.Map<List<MessageListItemModel>>(result.Items));
        }

        [HttpGet("friends")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFriends([FromQuery]InfiniteScroll scroll)
        {
            return Ok(await Chat.GetFriends(scroll, User.Id()));
        }

        [HttpGet("search-friends/{searchInput}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SearchFriends(string searchInput)
        {
            return Ok(await Chat.SearchFriends(searchInput, User.Id()));
        }

        [HttpPost("{receiverId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> WriteMessage(long receiverId, [FromBody]MessageModel model)
        {
            if (!await Users.CheckIfExists(receiverId)) return NotFound();
            if (!await Friendships.CheckIfFriendshipExist(User.Id(), receiverId)) return NotFound();

            await Chat.WriteMessage(User.Id(), Mapper.Map<Message>(model));

            await HubContext.Clients.Users(await SignalRService.GetUserConnections(receiverId))
                .ChatMessageReceived(User.Id(), model.MessageText);

            return Ok();
        }
    }
}