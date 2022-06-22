using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebSolution.DAL.Entities.Base;
using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.DAL.Entities
{
    public class DataValue : Entity
    {

        public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;

        public string Value { get; set; }

        public DataSource Source { get; set; }

        bool IsFaultead { get; set; }
    }
}
