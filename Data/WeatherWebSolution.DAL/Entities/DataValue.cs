using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Entities.Base;

namespace WeatherWebSolution.DAL.Entities
{
    [Index(nameof(Time))]
    public class DataValue : Entity
    {

        //Время регистрации значения
        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        //Регистрируемое значение
        public string Value { get; set; }

        //Источник данных
        public DataSource? Source { get; set; }

        //Флаг возникновения ошибки
        bool IsFaultead { get; set; }
    }
}
