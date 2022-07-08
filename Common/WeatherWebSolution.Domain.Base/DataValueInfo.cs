using System;
using System.Collections.Generic;
using System.Text;
using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.Domain.Base
{
    internal class DataValueInfo : IEntity
    {
        public int Id { get; }

        public DateTimeOffset Time { get; set; }

        public string Value { get; set; }

        bool IsFaultead { get; set; }
    }
}
