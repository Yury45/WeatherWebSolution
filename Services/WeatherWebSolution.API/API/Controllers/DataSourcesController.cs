using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using WeatherWebSolution.API.Controllers.Base;
using WeatherWebSolution.DAL.Context;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.API.Controllers
{
    public class DataSourcesController : EntityController<DataSource>
    {
        private readonly IRepository<DataSource> _Repository;


        public DataSourcesController(IRepository<DataSource> repository) : base(repository)
        {

        }

    }
}
