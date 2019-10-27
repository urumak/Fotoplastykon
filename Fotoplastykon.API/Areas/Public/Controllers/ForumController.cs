using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Areas.Public.Models.Forum;
using Fotoplastykon.API.Extensions;
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
        private IMapper Mapper { get; }

        public ForumController(IForumService forumThreads, IMapper mapper)
        {
            ForumThreads = forumThreads;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedList([FromQuery]IPager pager)
        {
            var result = await ForumThreads.GetList(pager);

            return Ok(new PaginationResult<ForumThreadModel>
            {
                Pager = result.Pager,
                Items = Mapper.Map<List<ForumThreadModel>>(result.Items)
            });
        }

        [HttpGet("film")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedListForFilm([FromQuery]IPager pager, long filmId)
        {
            var result = await ForumThreads.GetListForFilm(pager, filmId);

            return Ok(new PaginationResult<ForumThreadModel>
            {
                Pager = result.Pager,
                Items = Mapper.Map<List<ForumThreadModel>>(result.Items)
            });
        }

        [HttpGet("film-person")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedListForFilmPerson([FromQuery]IPager pager, long personId)
        {
            var result = await ForumThreads.GetListForFilmPerson(pager, personId);

            return Ok(new PaginationResult<ForumThreadModel>
            {
                Pager = result.Pager,
                Items = Mapper.Map<List<ForumThreadModel>>(result.Items)
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(long id)
        {
            var result = await ForumThreads.Get(id);
            if (result == null) return NotFound();

            return Ok(Mapper.Map<ForumThreadModel>(result));
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Add([FromBody]ForumThreadCommentModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.ForumThreadId)) return NotFound();
            if (model.ParentId.HasValue && !await ForumThreads.CheckIfExists(model.ParentId.Value)) return NotFound();

            await ForumThreads.AddComment(Mapper.Map<ForumThreadComment>(model), User.Id());

            return Ok();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody]ForumThreadCommentModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.UpdateComment(model.Id, Mapper.Map<ForumThreadComment>(model));

            return Ok();
        }

        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove([FromBody]DeleteItemModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.Remove(model.Id);

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddComment([FromBody]ForumThreadCommentModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.ForumThreadId)) return NotFound();
            if (model.ParentId.HasValue && !await ForumThreads.CheckIfExists(model.ParentId.Value)) return NotFound();

            await ForumThreads.AddComment(Mapper.Map<ForumThreadComment>(model), User.Id());

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateComment([FromBody]ForumThreadCommentModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.UpdateComment(model.Id, Mapper.Map<ForumThreadComment>(model));

            return Ok();
        }

        [HttpDelete("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveComment([FromBody]DeleteItemModel model)
        {
            if (!await ForumThreads.CheckIfExists(model.Id)) return NotFound();

            await ForumThreads.RemoveComment(model.Id);

            return Ok();
        }
    }
}