using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpPost("invite/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Invite(long id)
        {
            Users.InviteFriend(User.Id(), id);
            return Ok();
        }

        [HttpPost("invitation-reply")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult AcceptInvitation([FromBody]InvitationReplyModel model)
        {
            if(model.Reply == InvitationReply.Accept)
            {
                Users.AcceptInvitation(User.Id(), model.InvingId);
            }

            if (model.Reply == InvitationReply.Refuse)
            {
                Users.RefuseInvitation(User.Id(), model.InvingId);
            }

            return Ok();
        }

        [HttpDelete("remove-friend/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult RemoveFriend(long id)
        {
            Users.RemoveFriend(User.Id(), id);
            return Ok(id);
        }
    }
}