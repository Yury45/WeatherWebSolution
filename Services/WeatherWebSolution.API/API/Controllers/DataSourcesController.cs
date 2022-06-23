﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : ControllerBase
    {
        private readonly IRepository<DataSource> _Repository;

        public DataSourcesController(IRepository<DataSource> Repository)
        {
            _Repository = Repository;
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _Repository.GetCount());

        [HttpGet("exist/id{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async  Task<IActionResult> ExistId(int id) => await _Repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _Repository.GetAll());

        [HttpGet("items[[{skip:int}-{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>>Get(int skip, int count) =>
        Ok(await _Repository.Get(skip, count));

    }
}
