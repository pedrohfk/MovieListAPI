using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MovieList.Core.AutoMapper;
using MovieList.Persistencia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MovieListAPI.Test
{
    public class TestConfig : IDisposable
    {
        public DbContextPersistencia DbContext { get; private set; }
        public IMediator Mediator { get; private set; }
        public IMapper Mapper { get; private set; }
        public IServiceCollection services { get; set; }

        public IConfiguration configuration { get; set; }

        public TestConfig()
        {
            this.Mediator = new Mock<IMediator>().Object;

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            Mapper = mapperConfig.CreateMapper();

            services.AddDbContext<DbContextPersistencia>(options => options.UseSqlite(configuration.GetConnectionString("SqlLiteConnection")));

            var options = new DbContextOptionsBuilder<DbContextPersistencia>()
                .UseSqlite(Guid.NewGuid().ToString())
                .Options;

            DbContext = new DbContextPersistencia(options);

            try
            {
                var CurrentDirectory = Path.GetFullPath(".");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower(),
                    Delimiter = ";"
                };
                using (var reader = new StreamReader(@$"{CurrentDirectory}\File\movielist.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    var records = csv.GetRecords<MovieList.Domain.Entities.MovieList>();

                    List<MovieList.Domain.Entities.MovieList> movies = csv.GetRecords<MovieList.Domain.Entities.MovieList>().ToList();

                    DbContext.MovieList.AddRange(movies);
                    DbContext.SaveChanges();
                }
            }
            catch (System.Exception e)
            {

                throw;
            }

        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }

    }
}
