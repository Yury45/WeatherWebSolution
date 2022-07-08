using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers.Base
{
    [ApiController, Route("api/[controller]")]
    public abstract class MappedEntityController<T, TBase> : ControllerBase
    where TBase : IEntity 
    where T : IEntity
    {
        private readonly IRepository<TBase> _repository;
        private readonly IMapper _mapper;

        public MappedEntityController(IRepository<TBase> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region Mapping Func: T <-> TBase Mapping Func

        protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);

        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => _mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItems(IEnumerable<TBase> items) => _mapper.Map<IEnumerable<T>>(items);

        #endregion

        #region Add : Метод добавление сущности item в БД

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await _repository.Add(GetBase(item)).ConfigureAwait(false);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));

        }
        #endregion

        #region Delete : Метод удаления сущности item из БД

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(T item)
        {
            if (await _repository.Delete(GetBase(item)) is not { } result)
                return NotFound(item);
            return Ok(GetItem(result));
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
            return Ok(GetItem(result));
        }

        #endregion

        #region Exist : Метод определяющий наличие в БД элемента item

        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(T item) => await _repository.Exist(GetBase(item)) ? Ok(true) : NotFound(false);

        #endregion

        #region ExistId : Метод определяющий наличие в БД элемента с id

        [HttpGet("exist/id{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id) => await _repository.ExistId(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(GetItems(await _repository.GetAll()));

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
            Ok(GetItems(await _repository.Get(skip, count)));

        #endregion

        #region  GetItemsCount : Метод получения общего количества элементов в БД

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount() => Ok(await _repository.GetCount());

        #endregion

        #region GetPage : Метод получения страницы IPage с индесом pageIndex и размером pageSize

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>
        {
            public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        }

        protected IPage<T> GetItems(IPage<TBase> page) =>
            new Page(GetItems(page.Items), page.TotalCount, page.PageIndex, page.PageSize);

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page/[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<T>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPage(pageIndex, pageSize);
            return result.Items.Any()
                ? Ok(GetItems(result))
                : NotFound(GetItems(result));
        }
        #endregion

        #region Update : Метод обновления сущности item в БД по id

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            if (await _repository.Update(GetBase(item)) is not { } result)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        #endregion
    }
}
