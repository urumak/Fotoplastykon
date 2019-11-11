using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Forum;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/forum")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private IForumService ForumThreads { get; }

        public ForumController(IForumService forumThreads)
        {
            ForumThreads = forumThreads;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedList([FromQuery]IPager pager)
        {
            return Ok(await ForumThreads.GetList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(long id)
        {
            var result = await ForumThreads.Get(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Add([FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfExists(model.ForumThreadId)) return NotFound();
            if (model.ParentId.HasValue && !await ForumThreads.CheckIfExists(model.ParentId.Value)) return NotFound();

            await ForumThreads.AddComment(model, User.Id());

            return Ok();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.UpdateComment(model.Id, model);

            return Ok();
        }

        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove([FromBody]ItemIdModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.Remove(model.Id);

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddComment([FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfExists(model.ForumThreadId)) return NotFound();
            if (model.ParentId.HasValue && !await ForumThreads.CheckIfExists(model.ParentId.Value)) return NotFound();

            await ForumThreads.AddComment(model, User.Id());

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateComment([FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.UpdateComment(model.Id, model);

            return Ok();
        }

        [HttpDelete("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveComment([FromBody]ItemIdModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.RemoveComment(model.Id);

            return Ok();
        }
    }
}