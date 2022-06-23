using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Context;
using WeatherWebSolution.DAL.Entities.Base;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.DAL.Repositories
{
    public class DbNamedRepository<T>: DbRepository<T>, INamedRepository<T> where T : NamedEntity, new ()
    {
        public DbNamedRepository(DataDB db) : base(db) { }

        public async Task<T> DeleteByName(string name, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Name == name);
            if (item is null)
                item = await Set
                    .Select(i => new T { Id = i.Id, Name = i.Name })
                    .FirstOrDefaultAsync(i => i.Name == name)
                    .ConfigureAwait(false);
            if (item is null)
                return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistName(string name, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(i => i.Name == name, cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByName(string name, CancellationToken cancel = default)
        {
            return await Items.FirstOrDefaultAsync(i => i.Name == name, cancel).ConfigureAwait(false);
        }

    }
}
