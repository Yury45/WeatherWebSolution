using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Entities.Base;
using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class DataSource : NamedEntity
    {
        public string Description { get; set; }
    }
}
