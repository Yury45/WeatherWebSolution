using AutoMapper;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Domain.Base;

namespace WeatherWebSolution.API.Infrastructure.AutoMapper
{
    public class DataSourceMap : Profile
    {
        public DataSourceMap()
        {
            CreateMap<DataSourceInfo, DataSource>()
                .ReverseMap();
        }
    }
}
