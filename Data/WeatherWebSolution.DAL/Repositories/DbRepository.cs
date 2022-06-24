using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Context;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.DAL.Entities.Base;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataDB _db;

        protected virtual IQueryable<T> Items => Set;

        public bool AuroSaveChanges { get; set; } = true;

        protected DbSet<T> Set { get; }

        public DbSet<DataValue> Values { get; set; }

        public DbSet<DataSource> Sources { get; set; }

        public DbRepository(DataDB db)
        {
            _db = db;
            Set = _db.Set<T>();
        }

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            await _db.AddAsync(item, cancel).ConfigureAwait(false);

            if (AuroSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);

            return item;
        }

        public async Task<T> Delete(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await Exist(item, cancel).ConfigureAwait(false))
                return null;

            _db.Remove(item);
            if (AuroSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(i => i.Id == id);
            if(item is null)
                item = await Set
                    .Select(i => new T { Id= i.Id})
                    .FirstOrDefaultAsync(i=> i.Id == id)
                    .ConfigureAwait(false);
            if (item is null)
                return null;

            return await Delete(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return await Items.AnyAsync(i => i.Id == item.Id, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistId(int id, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == id, cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            if (count <= 0)
                return Enumerable.Empty<T>();

            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> orderedQueryable => orderedQueryable,

                { } unorderedQueryable => unorderedQueryable.OrderBy(i => i.Id)
            };
            if (skip > 0)
                query = query.Skip(skip);
            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            return await Items.ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<T> GetById(int id, CancellationToken cancel = default)
        {
            switch (Items)
            {
                case DbSet<T> set:
                    return await set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false);

                case IQueryable<T> items:
                    return await items.FirstOrDefaultAsync(i => i.Id == id, cancel).ConfigureAwait(false);

                default:
                    throw new InvalidOperationException("Ошибка в определении источника данных");
            }
        }

        public async Task<int> GetCount(CancellationToken cancel = default)
        {
            return await Items.CountAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IPage<T>> GetPage(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            if (pageSize <= 0) return new Page(Enumerable.Empty<T>(), pageSize, pageIndex, pageSize);

            var query = Items;
            var totalCount = await query.CountAsync(cancel).ConfigureAwait(false);
            if (totalCount == 0)
                return new Page(Enumerable.Empty<T>(), 0, pageIndex, pageSize);

            query = Items switch
            {
                IOrderedQueryable<T> orderedQueryable => orderedQueryable,

                { } unorderedQueryable => unorderedQueryable.OrderBy(i => i.Id)
            };

            if (pageIndex > 0)
                query = query.Skip(pageIndex * pageSize);
            query = query.Take(pageSize);

            var items = await query.ToArrayAsync(cancel).ConfigureAwait(false);
            return new Page(items, totalCount, pageIndex, pageSize);

        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Update(item);
            if (AuroSaveChanges)
                await SaveChanges(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<int> SaveChanges(CancellationToken cancel = default)
        {
            return await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;
    }
}
