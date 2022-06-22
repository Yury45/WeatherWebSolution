using WeatherWebSolution.Intefaces.Base.Entities;

namespace WeatherWebSolution.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

    }
}
