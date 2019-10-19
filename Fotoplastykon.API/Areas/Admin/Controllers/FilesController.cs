using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}