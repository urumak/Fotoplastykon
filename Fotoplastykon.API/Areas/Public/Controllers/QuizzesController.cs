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

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/quizzes")]
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
        public async Task<IActionResult> Get(long id)
        {
            var result = await Quizzes.GetFull(id);
            if (result == null) return NotFound();

            return Ok(Mapper.Map<QuizModel>(result));
        }

        [HttpPost("submit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SubmitQuiz(long id, [FromBody]QuizModel quizModel)
        {
            if (!await Quizzes.CheckIfQuizExists(id)) return NotFound();

            return Ok(await Quizzes.SubmitQuiz(id, User.Id(), quizModel));
        }
    }
}