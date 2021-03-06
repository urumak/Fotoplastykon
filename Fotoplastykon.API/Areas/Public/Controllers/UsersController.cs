﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fotoplastykon.API.Areas.Public.Models;
using Fotoplastykon.API.Areas.Public.Models.Users;
using Fotoplastykon.API.Extensions;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fotoplastykon.API.Areas.Public.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService Users { get; }
        private IMapper Mapper { get; }

        public UsersController(IUsersService users, IMapper mapper)
        {
            Users = users;
            Mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUser(long id)
        {
            var page = await Users.GetForPage(id, User.Id());
            if (page == null) return NotFound();
            return Ok(page);
        }
    }
}