using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebSolution.Intefaces.Base.Entities.Reposytories
{
    public interface INamedRepository<T> : IRepository<T> where T: INamedEntity
    {
        //Метод возвращает факт наличия cущности по указанному name
        Task<bool> ExistName(string name, CancellationToken cancel = default);

        //Метод возвращает cущность по указанному name
        Task<T> GetByName(string name, CancellationToken cancel = default);

        //Метод возвращает с удалением cущность по указанному name
        Task<T> DeleteByName(string name, CancellationToken cancel = default);

    }
}
