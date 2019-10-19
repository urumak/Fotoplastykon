using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmsController : ControllerBase
    {

        public FilmsController()
        {

        }
        [Route("{id}")]
        public IActionResult GetAll(string id)
        {
            return Ok();
        }

        [HttpPost("rate/{filmPublicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult SubmitQuiz(string filmPublicId)
        {
            return Ok();
        }
    }
}