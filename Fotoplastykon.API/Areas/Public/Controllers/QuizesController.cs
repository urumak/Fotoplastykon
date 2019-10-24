﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models.Quizes;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.DTOs.Quizes;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/quizes")]
    [ApiController]
    public class QuizesController : ControllerBase
    {
        private IQuizesService Quizes { get; }
        private IMapper Mapper { get; }

        public QuizesController(IQuizesService quizes, IMapper mapper)
        {
            Quizes = quizes;
            Mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPaginatedList([FromQuery]Pager pager)
        {
            var result = await Quizes.GetPaginatedList(pager);

            return Ok(new PaginationResult<ListItemModel>
            {
                Pager = result.Pager,
                Items = Mapper.Map<List<ListItemModel>>(result.Items)
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Get(long id)
        {
            var result = await Quizes.GetFull(id);
            if (result == null) return NotFound();

            return Ok(Mapper.Map<QuizModel>(result));
        }

        [HttpPost("submit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SubmitQuiz(long id, [FromBody]List<UserAnswerDTO> answers)
        {
            if (!await Quizes.CheckIfQuizExists(id)) return NotFound();

            return Ok(await Quizes.SubmitQuiz(id, User.Id(), answers));
        }
    }
}