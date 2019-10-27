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
            await Pages.GetUserPage(id);
            return Ok();
        }

        [HttpGet("film/{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilm(string id)
        {
            await Pages.GetFilmPage(id);
            return Ok();
        }

        [HttpGet("film-person/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetFilmPerson(string id)
        {
            await Pages.GetFilmPersonPage(id);
            return Ok();
        }
    }
}