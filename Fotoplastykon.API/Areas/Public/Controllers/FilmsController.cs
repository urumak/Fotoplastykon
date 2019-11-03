﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Films;
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

        [HttpGet("get-rate/{filmId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRate(long filmId)
        {
            if (!await Films.CheckIfExists(filmId)) return NotFound();
            return Ok(await Films.GetRate(User.Id(), filmId));
        }

        [HttpPost("rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Rate(FilmMarkDTO model)
        {
            if (!await Films.CheckIfExists(model.FilmId)) return NotFound();
            if (await Films.CheckIfWatchingExists(User.Id(), model.FilmId)) return BadRequest("Użytkownik ocenił już film");

            model.UserId = User.Id();
            await Films.Rate(model);

            return Ok();
        }
    }
}