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
        public async Task<IActionResult> GetPaginatedList([FromQuery]Pager pager)
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
        public async Task<IActionResult> Add([FromBody]ForumThreadDTO model)
        {
            return Ok(await ForumThreads.Add(model, User.Id()));
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, [FromBody]ForumThreadDTO model)
        {
            if (!await ForumThreads.CheckIfExists(id)) return NotFound();

            await ForumThreads.Update(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await ForumThreads.CheckIfExists(id)) return NotFound();

            await ForumThreads.Remove(id);

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddComment([FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfExists(model.ThreadId)) return NotFound();
            if (model.ParentId.HasValue && !await ForumThreads.CheckIfCommentExists(model.ParentId.Value)) return NotFound();

            await ForumThreads.AddComment(model, User.Id());

            return Ok();
        }

        [HttpPost("comment/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateComment(long id, [FromBody]ForumThreadCommentDTO model)
        {
            if (!await ForumThreads.CheckIfCommentExists(id)) return NotFound();

            await ForumThreads.UpdateComment(id, model);

            return Ok();
        }

        [HttpDelete("comment/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveComment(long id)
        {
            if (!await ForumThreads.CheckIfCommentExists(id)) return NotFound();

            await ForumThreads.RemoveComment(id);

            return Ok();
        }
    }
}