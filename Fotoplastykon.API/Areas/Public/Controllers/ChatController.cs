﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Chat;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public ChatController(IChatService chat, IMapper mapper, IUsersService users, IFriendshipsService friendships)
        {
            Chat = chat;
            Mapper = mapper;
            Users = users;
            Friendships = friendships;
        }

        [HttpGet("{friendId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUser(IPager pager, long friendId)
        {
            if (!await Friendships.CheckIfFriendshipExist(User.Id(), friendId)) return NotFound();
            var result = await Chat.GetMessages(pager, User.Id(), friendId);

            return Ok(Mapper.Map<List<MessageListItemModel>>(result.Items));
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> WriteMessage([FromBody]MessageModel model)
        {
            if (!await Users.CheckIfExists(model.ReceiverId)) return NotFound();
            if (!await Friendships.CheckIfFriendshipExist(User.Id(), model.ReceiverId)) return NotFound();

            await Chat.WriteMessage(User.Id(), Mapper.Map<Message>(model));
            return Ok();
        }
    }
}