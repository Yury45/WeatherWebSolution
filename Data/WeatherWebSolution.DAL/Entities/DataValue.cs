using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Entities.Base;

namespace WeatherWebSolution.DAL.Entities
{
    [Index(nameof(Time))]
    public class DataValue : Entity
    {

        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource? Source { get; set; }

        bool IsFaultead { get; set; }
    }
}
