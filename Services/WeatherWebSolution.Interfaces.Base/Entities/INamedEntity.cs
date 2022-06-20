using System.ComponentModel.DataAnnotations;

namespace WeatherWebSolution.Intefaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}


