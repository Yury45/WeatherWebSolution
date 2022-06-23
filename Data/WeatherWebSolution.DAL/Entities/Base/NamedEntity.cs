using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.DAL.Entities.Base
{

    public abstract class NamedEntity :Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
