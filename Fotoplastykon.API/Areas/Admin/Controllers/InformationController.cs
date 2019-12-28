using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.BLL.DTOs.Information;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/information")]
    [ApiController]
    [Authorize(Policy = "AdminAccess")]
    public class InformationController : ControllerBase
    {
        protected IInformationsService Information { get; }
        protected IMapper Mapper { get; }

        public InformationController(IInformationsService information, IMapper mapper)
        {
            Information = information;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetList([FromQuery]Pager pager)
        {
            return Ok(await Information.GetPaginatedList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Fetch(long id)
        {
            var user = await Information.Fetch(id);
            if (user == null) return NotFound();

            return Ok(user);
        }


        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, [FromBody]InformationFormModel model)
        {
            if (!await Information.CheckIfExists(id)) return NotFound();
            await Information.Update(id, model);

            return Ok(await Information.Fetch(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await Information.CheckIfExists(id)) return NotFound();

            await Information.Remove(id);

            return Ok();
        }

        [HttpPost("change-photo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangeProfilePhoto(long id, [FromForm]IFormFile file)
        {
            await Information.ChangePhoto(id, file);
            return Ok(await Information.Fetch(id));
        }
    }
}