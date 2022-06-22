using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.DAL.Entities.Base
{

    public abstract class NamedEntity :Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
