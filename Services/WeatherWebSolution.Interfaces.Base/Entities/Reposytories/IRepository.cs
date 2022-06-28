using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherWebSolution.Intefaces.Base.Entities.Reposytories
{
    public interface IRepository<T> where T : IEntity
    {
        //Метод возвращает факт наличия cущности по Id
        Task<bool> ExistId(int id, CancellationToken cancel = default);

        //Метод возвращает факт наличия cущности T целиком
        Task<bool> Exist(T item, CancellationToken cancel = default);

        //Метод возвращает общегее количество сущностей, либо null
        Task<int> GetCount(CancellationToken cancel = default);

        //Метод возвращает перечисление всех сущностей, либо null
        Task<IEnumerable<T>> GetAll(CancellationToken cancel = default);

        //Метод возвращает перечесление некоторого количества (count) сущностей с определенного номера (skip) от начала, либо null
        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default);

        //Метод возвращает определенную страницу заданного размера, либо null
        Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default);

        //Метод возвращает cущность по Id, либо null
        Task<T> GetById(int id, CancellationToken cancel = default);

        //Метод сохраняет в Repository и возвращает переданную в него cущность, либо null
        Task<T> Add(T item, CancellationToken cancel = default);

        //Метод обновляет и возвращает возвращает переданную в него cущность, либо null
        Task<T> Update(T item, CancellationToken cancel = default);

        //Метод удаляет сущность из Repository и возвращает удаленный элемент, либо null
        Task<T> Delete(T item, CancellationToken cancel = default);

        //Метод удаляет сущность из Repository по Id и возвращает удаленный элемент, либо null
        Task<T> DeleteById(int id, CancellationToken cancel = default);
    }

    public interface IPage<T>
    {
        //Метод возвращает перечисление всех элементов
        IEnumerable<T> Items { get;  }

        //свойство - возвращает количество всех элементов перечисления 
        int TotalCount { get; }

        //свойство - возвращает номер страницы
        int PageIndex { get;  }

        //свойство - возвращает размер страницы
        int PageSize { get;  }

        //свойство возвращает общее количество страниц
        int TotalPages { get; }

        //Метод возвращает общее количество страниц
        //int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);

    }
}
