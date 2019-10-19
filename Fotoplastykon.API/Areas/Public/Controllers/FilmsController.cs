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
        [Route("{id}")]
        public IActionResult GetAll(string id)
        {
            return Ok();
        }

        [HttpPost("rate/{filmId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Rate(long filmId)
        {
            if (!Films.CheckIfExists(filmId)) return NotFound();
            if (Films.CheckIfWatchingExists(User.Id(), filmId)) return BadRequest("Użytkownik ocenił już film");
            return Ok();
        }
    }
}