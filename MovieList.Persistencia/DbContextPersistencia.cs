using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MovieList.Persistencia
{
    public class DbContextPersistencia : DbContext
    {
        private static bool _created = false;

        public DbContextPersistencia(DbContextOptions<DbContextPersistencia> options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public DbSet<Domain.Entities.MovieList> MovieList { get; set; }

        public DbSet<Domain.Entities.IntervaloPremio.Min> Min { get; set; }

        public DbSet<Domain.Entities.IntervaloPremio.Max> Max { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.MovieList>(entity =>
            {
                entity.HasKey(i => new { i.id } );
            });

            modelBuilder.Entity<Domain.Entities.IntervaloPremio.Min>(entity =>
            {
                entity.HasKey(i => new { i.id });
            });

            modelBuilder.Entity<Domain.Entities.IntervaloPremio.Max>(entity =>
            {
                entity.HasKey(i => new { i.id });
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
