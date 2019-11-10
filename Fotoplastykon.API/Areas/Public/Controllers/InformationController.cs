using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Information;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/information")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private IInformationsService Informations { get; }

        public InformationController(IInformationsService informations)
        {
            Informations = informations;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedList([FromQuery]Pager pager)
        {
            return Ok(await Informations.GetPaginatedList(pager));
        }

        [HttpGet("main-page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetListForMainPage()
        {
            return Ok(await Informations.GetListForMainPage());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(long id)
        {
            var result = await Informations.GetWithCreatorAndComments(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddComment([FromBody]CommentFormDTO model)
        {
            if (!await Informations.CheckIfExists(model.InformationId)) return NotFound();
            if (model.ParentId.HasValue && !await Informations.CheckIfExists(model.ParentId.Value)) return NotFound();

            await Informations.AddComment(model, User.Id());

            return Ok();
        }

        [HttpPost("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateComment([FromBody]CommentFormDTO model)
        {
            if (!await Informations.CheckIfExists(model.Id)) return NotFound();

            await Informations.UpdateComment(model.Id, model);

            return Ok();
        }

        [HttpDelete("comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateComment([FromBody]ItemIdModel model)
        {
            if (!await Informations.CheckIfExists(model.Id)) return NotFound();

            await Informations.RemoveComment(model.Id);

            return Ok();
        }
    }
}