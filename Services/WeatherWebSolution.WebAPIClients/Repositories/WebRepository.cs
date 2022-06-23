using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebSolution.Intefaces.Base.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.WebAPIClients.Repositories
{
    internal class WebRepository<T> : IRepository<T> where T : IEntity
    {
        public Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Add(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
