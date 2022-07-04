using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.DAL.Entities.Base;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class EntityController<T> : ControllerBase where T : Entity
    {
        private readonly IRepository<T> _repository;

        protected EntityController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(item).ConfigureAwait(false);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);

        }

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page/[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return result.Items.Any()
                ? Ok(result)
                : NotFound(result);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _repository.GetCount());

        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(T item) => await _repository.Exist(item) ? Ok(true) : NotFound(false);

        [HttpGet("exist/id{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id) => await _repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        [HttpGet("{id:int}")]
        [ActionName("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("items[[{skip:int}-{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) =>
        Ok(await _repository.Get(skip, count));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _repository.Update(item) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
        {
            if (await _repository.Delete(item) is not { } result)
                return NotFound(item);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }
    }
}
