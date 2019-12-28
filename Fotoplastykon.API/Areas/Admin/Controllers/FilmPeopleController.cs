using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.BLL.DTOs.FilmPeople;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/film-people")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class FilmPeopleController : ControllerBase
    {
        protected IFilmPeopleService People { get; }
        protected IMapper Mapper { get; }

        public FilmPeopleController(IFilmPeopleService people, IMapper mapper)
        {
            People = people;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList([FromQuery]Pager pager)
        {
            return Ok(await People.GetList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Fetch(long id)
        {
            var person = await People.Fetch(id);
            if (person == null) return NotFound();

            return Ok(person);
        }


        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, [FromBody]FilmPersonFormModel model)
        {
            if (!await People.CheckIfExists(id)) return NotFound();
            await People.Update(id, model);

            return Ok(await People.Fetch(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await People.CheckIfExists(id)) return NotFound();

            await People.Remove(id);

            return Ok();
        }

        [HttpPost("change-photo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangePhoto(long id, [FromForm]IFormFile file)
        {
            await People.ChangePhoto(id, file);
            return Ok(await People.Fetch(id));
        }
    }
}