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


        [HttpPost("rate/{perdonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Rate(long perdonId)
        {
            if (!FilmPeople.CheckIfExists(perdonId)) return NotFound();
            if (FilmPeople.CheckIfWatchingExists(User.Id(), perdonId)) return BadRequest("Użytkownik ocenił już osobę");
            return Ok();
        }
    }
}