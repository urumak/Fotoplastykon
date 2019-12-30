using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Admin.Controllers
{
    [Route("api/admin/quizzes")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private IQuizzesService Quizzes { get; }
        private IMapper Mapper { get; }

        public QuizzesController(IQuizzesService quizzes, IMapper mapper)
        {
            Quizzes = quizzes;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedList([FromQuery]Pager pager)
        {
            return Ok(await Quizzes.GetPaginatedList(pager));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Fetch(long id)
        {
            var quiz = await Quizzes.Fetch(id);
            if (quiz == null) return NotFound();

            return Ok(quiz);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, [FromBody]QuizFormModel model)
        {
            if (!await Quizzes.CheckIfQuizExists(id)) return NotFound();
            await Quizzes.Update(id, model);

            return Ok(await Quizzes.Fetch(id));
        }

        [HttpPost("change-photo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ChangPhoto(long id, [FromForm]IFormFile file)
        {
            await Quizzes.ChangePhoto(id, file);
            return Ok(await Quizzes.Fetch(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Remove(long id)
        {
            if (!await Quizzes.CheckIfQuizExists(id)) return NotFound();

            await Quizzes.Remove(id);

            return Ok();
        }
    }
}