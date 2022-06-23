using Microsoft.EntityFrameworkCore;
using WeatherWebSolution.DAL.Entities;

namespace WeatherWebSolution.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<DataValue> Values { get; set; }
        public DbSet<DataSource> Sources { get; set; }

        public DataDB(DbContextOptions<DataDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne(v => v.Source)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
