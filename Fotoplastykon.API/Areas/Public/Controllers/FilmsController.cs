using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Films;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        protected IFilmsService Films { get; }
        public FilmsController(IFilmsService films)
        {
            Films = films;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilm(long id)
        {
            var page = await Films.GetForPage(id, User.Id());
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpGet("rating/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRating(long id)
        {
            if (!await Films.CheckIfExists(id)) return NotFound();
            return Ok(await Films.GetRating(id));
        }

        [HttpPost("rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Rate([FromBody]FilmMarkDTO model)
        {
            if (!await Films.CheckIfExists(model.FilmId)) return NotFound();

            model.UserId = User.Id();
            await Films.Rate(model);

            return Ok();
        }


        [HttpGet("watched-films/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetWatchedFilms(long userId, [FromQuery]Pager pager)
        {
            return Ok(await Films.GetPaginatedListForUser(pager, userId));
        }
    }
}