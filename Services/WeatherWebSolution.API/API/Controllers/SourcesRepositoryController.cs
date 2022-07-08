using AutoMapper;
using WeatherWebSolution.API.Controllers.Base;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Domain.Base;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers
{
    public class SourcesRepositoryController : MappedEntityController<DataSourceInfo, DataSource>
    {
        public SourcesRepositoryController(IRepository<DataSource> repository, IMapper mapper) 
            : base(repository, mapper)
        {
        }
    }
}
