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
    [Authorize(Policy = "CanEditPages")]
    public class FilmsController : ControllerBase
    {
        [Route("{id}")]
        public IActionResult GetAll(string id)
        {
            return Ok();
        }
    }
}