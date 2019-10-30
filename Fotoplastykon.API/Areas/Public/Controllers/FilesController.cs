using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        protected IFilesService Files { get; }
        public FilesController(IFilesService files)
        {
            Files = files;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Add([FromForm]IFormFile file)
        {
            await Files.Add(file);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(long id)
        {
            var file = await Files.Get(id);
            if (file == null || file.Content.Length == 0) return NotFound();
            return File(file.Content, file.ContentType, file.FileName);
        }

        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove([FromBody]ItemIdModel model)
        {
            if (!await Files.CheckIfExists(model.Id)) return NotFound();
            await Files.Remove(model.Id);
            return Ok();
        }
    }
}