using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/films")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class FilmsController : ControllerBase
    {
        protected IFilmsService Films { get; }
        protected IMapper Mapper { get; }

        public FilmsController(IFilmsService films, IMapper mapper)
        {
            Films = films;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList([FromQuery]Pager pager)
        {
            return Ok(await Films.GetPaginatedList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Fetch(long id)
        {
            var film = await Films.Fetch(id);
            if (film == null) return NotFound();

            return Ok(film);
        }


        //[HttpPost("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> Update(long id, [FromBody]FilmFomModel model)
        //{
        //    if (!await Films.CheckIfExists(id)) return NotFound();
        //    await Films.Update(id, model);

        //    return Ok(await Films.Fetch(id));
        //}

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await Films.CheckIfExists(id)) return NotFound();

            await Films.Remove(id);

            return Ok();
        }

        [HttpPost("change-photo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeProfilePhoto(long id, [FromForm]IFormFile file)
        {
            await Films.ChangePhoto(id, file);
            return Ok(await Films.Fetch(id));
        }

        [HttpGet("role-types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRoleTypes()
        {
            return Ok(Films.GetRoleTypes());
        }

        [HttpGet("people/{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilmPeople(string search)
        {
            return Ok(await Films.GetFilmmakers(search));
        }

        [HttpGet("person/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilmPerson(long id)
        {
            return Ok(await Films.GetForSearch(id));
        }
    }
}