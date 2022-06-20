using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebSolution.DAL.Entities;

namespace WeatherWebSolution.DAL.Context
{
    internal class DataDB : DbContext
    {
        public DbSet<DataValue> Values { get; set; }
        public DbSet<DataSource> Sources { get; set; }

        public DataDB(DbContextOptions<DataDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
