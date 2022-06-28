using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Entities.Base;

namespace WeatherWebSolution.DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class DataSource : NamedEntity
    {
        //Описание источника данных
        public string Description { get; set; }
    }
}
