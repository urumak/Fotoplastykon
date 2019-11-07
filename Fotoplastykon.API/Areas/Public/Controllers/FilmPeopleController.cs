using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.Services.Abstract;
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
    }
}