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
        public async Task<IActionResult> GetItemsCount() => Ok(await _Repository.GetCount());
    }
}
