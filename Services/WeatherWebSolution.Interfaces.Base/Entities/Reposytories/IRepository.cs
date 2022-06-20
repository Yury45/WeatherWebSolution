using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWebSolution.Intefaces.Base.Entities.Reposytories
{
    internal interface IRepository<T>where T : IEntity
    {
        Task<bool> ExistId(int id);

        Task<bool> Exist(T item);

        Task<int> GetCount();

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Get(int skip, int count);

        Task<IPage<T>> GetPage(int pageIndex, int pageSize);

        Task<T> GetById(int id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(T item);

        Task<T> DeleteById(int id);
    }

    public interface IPage<T>
    {
        IEnumerable<T> Items();

        int TotalCount { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);

    }
}
