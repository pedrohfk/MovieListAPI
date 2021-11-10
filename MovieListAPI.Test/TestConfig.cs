using AutoMapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using MovieList.Core.AutoMapper;
using MovieList.Core.Init;
using MovieList.Persistencia;
using System;
using System.Threading;

namespace MovieListAPI.Test
{
    public class TestConfig : IDisposable
    {
        public DbContextPersistencia DbContexts { get; private set; }
        public IMediator Mediator { get; private set; }
        public IMapper Mapper { get; private set; }

        private readonly SqliteConnection _connection;

        private const string InMemoryConnectionString = "DataSource=:memory:";

        public TestConfig()
        {
            this.Mediator = new Mock<IMediator>().Object;

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            Mapper = mapperConfig.CreateMapper();


            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DbContextPersistencia>()
                    .UseSqlite(_connection)
                    .Options;
            DbContexts = new DbContextPersistencia(options);
            DbContexts.Database.EnsureCreated();

            var result = new InitHandler(DbContexts).Handle(new Init(), CancellationToken.None);
        }

        public void Dispose()
        {
            _connection.Close();
        }

    }
}
