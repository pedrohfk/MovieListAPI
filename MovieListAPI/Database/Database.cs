using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieList.Persistencia;
using System;

namespace MovieListAPI.Database
{
    public static class Database
    {
        public static IServiceCollection AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<DbContextPersistencia>(options => options.UseSqlite(configuration.GetConnectionString("SqlLiteConnection")));

            return services;
        }
    }
}
