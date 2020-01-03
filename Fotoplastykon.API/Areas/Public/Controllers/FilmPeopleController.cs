using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/film-people")]
    [ApiController]
    public class FilmPeopleController : ControllerBase
    {
        protected IFilmPeopleService FilmPeople { get; }
        public FilmPeopleController(IFilmPeopleService filmPeople)
        {
            FilmPeople = filmPeople;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList([FromQuery]Pager pager)
        {
            return Ok(await FilmPeople.GetList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilmPerson(long id)
        {
            var page = await FilmPeople.GetForPage(id, User.Id());
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpPost("rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Rate([FromBody]PersonMarkDTO model)
        {
            if (!await FilmPeople.CheckIfExists(model.PersonId)) return NotFound();

            model.UserId = User.Id();
            await FilmPeople.Rate(model);

            return Ok();
        }

        [HttpGet("rating/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRating(long id)
        {
            if (!await FilmPeople.CheckIfExists(id)) return NotFound();
            return Ok(await FilmPeople.GetRating(id));
        }

        [HttpGet("rated-people/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetWatchedFilms(long userId, [FromQuery]Pager pager)
        {
            return Ok(await FilmPeople.GetPaginatedListForUser(pager, userId));
        }

        [HttpGet("forum/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetForumThreads(long personId, [FromQuery]Pager pager)
        {
            return Ok(await FilmPeople.GetTheMostPopularForumThreads(pager, personId));
        }

        [HttpGet("person-roles/{personId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPersonRoles(long personId, [FromQuery]Pager pager)
        {
            return Ok(await FilmPeople.GetPersonRoles(pager, personId));
        }
    }
}