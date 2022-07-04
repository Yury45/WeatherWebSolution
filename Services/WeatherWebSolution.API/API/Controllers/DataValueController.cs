using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherWebSolution.API.Controllers.Base;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataValueController : EntityController<DataValue>
    {
        public DataValueController(IRepository<DataValue> repository) : base(repository)
        {

        }
    }
}
