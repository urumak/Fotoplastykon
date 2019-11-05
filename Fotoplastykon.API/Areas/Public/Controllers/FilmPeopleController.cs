using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
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
            var page = await FilmPeople.GetForPage(id);
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpPost("rate/{perdonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Rate(long perdonId)
        {
            if (!await FilmPeople.CheckIfExists(perdonId)) return NotFound();
            
            return Ok();
        }
    }
}