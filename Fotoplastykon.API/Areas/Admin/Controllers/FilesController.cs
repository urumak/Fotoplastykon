using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.API.Areas.Admin.Models.Files;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        protected IFileService Files { get; }
        public FilesController(IFileService files)
        {
            Files = files;
        }

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Add([FromForm]IFormFile file)
        {
            Files.Add(file);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult Get(long id)
        {
            var file = Files.Get(id);
            if (file == null || !file.Exists) return NotFound();
            return Ok();
        }

        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Remove([FromBody]FileIdModel model)
        {
            if (!Files.CheckIfExists(model.Id)) return NotFound();
            Files.Remove(model.Id);
            return Ok();
        }
    }
}