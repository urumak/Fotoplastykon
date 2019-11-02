using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/pages")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        protected IPagesService Pages { get; }
        protected IFilmsService Films { get; }
        protected IFilmPeopleService FilmPeople { get; }

        public PagesController(IPagesService pages, IFilmsService films, IFilmPeopleService filmPeople)
        {
            Pages = pages;
            Films = films;
            FilmPeople = filmPeople;
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUser(string id)
        {
            var page = await Pages.GetUserPage(id);
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpGet("film/{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilm(string id)
        {
            var page = await Pages.GetFilmPage(id);
            if (page == null) return NotFound();
            return Ok(page);
        }

        [HttpGet("film-person/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilmPerson(string id)
        {
            var page = await Pages.GetFilmPersonPage(id);
            if (page == null) return NotFound();
            return Ok(page);
        }
    }
}