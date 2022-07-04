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
        //Класс базового контроллера
        
        private readonly IRepository<T> _repository;

        protected EntityController(IRepository<T> repository)
        {
            _repository = repository;
        }

        #region Add : Метод добавление сущности item в БД

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(item).ConfigureAwait(false);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);

        }
        #endregion

        #region Delete : Метод удаления сущности item из БД

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
        {
            if (await _repository.Delete(item) is not { } result)
                return NotFound(item);
            return Ok(result);
        }

        #endregion

        #region DeleteById : Метод удаления сущности по id мз БД

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteById(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        #endregion

        #region Exist : Метод определяющий наличие в БД элемента item

        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(T item) => await _repository.Exist(item) ? Ok(true) : NotFound(false);

        #endregion

        #region ExistId : Метод определяющий наличие в БД элемента с id

        [HttpGet("exist/id{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id) => await _repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        #endregion

        #region Get : Метод получения сущносити из БД по id

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

        #endregion

        #region Get : Метод получения перечисления count сущностей из БД, пропустив skip от начала

        [HttpGet("items[[{skip:int}-{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count) =>
            Ok(await _repository.Get(skip, count));

        #endregion

        #region  GetItemsCount : Метод получения общего количества элементов в БД

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _repository.GetCount());

        #endregion

        #region GetPage : Метод получения страницы IPage с индесом pageIndex и размером pageSize

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
        #endregion

        #region Update : Метод обновления сущности item в БД по id

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _repository.Update(item) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        #endregion

    }
}
