using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
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
            var page = await Films.GetForPage(id);
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpPost("rate/{filmId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Rate(long filmId)
        {
            if (!await Films.CheckIfExists(filmId)) return NotFound();
            if (await Films.CheckIfWatchingExists(User.Id(), filmId)) return BadRequest("Użytkownik ocenił już film");
            return Ok();
        }
    }
}