using MovieList.Core.MovieList;
using MovieList.Core.MovieList.ListMovieList.Handlers.Queries;
using MovieList.Core.MovieList.ListWinnerMovieList.Queries;
using MovieList.Domain.Entities.IntervaloPremio;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MovieListAPI.Test
{
    public class MovieListTest : IClassFixture<TestConfig>
    {
        private readonly TestConfig TestConfig;

        public MovieListTest(TestConfig testConfig)
        {
            this.TestConfig = testConfig;
        }

        [Fact]
        public async Task GetProducersListWinners()
        {
            var query = new MovieListCommand();

            var request = new MovieListHandler(TestConfig.DbContexts);

            var result = await request.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetListMovieTest()
        {
            var query = new ListMovieCommand();

            var request = new ListMovieHandler(TestConfig.DbContexts, TestConfig.Mapper);

            var result = await request.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetWinnersProducersTest()
        {
            var query = new ListWinnerMovieListCommand();

            var request = new ListWinnerMovieListHandler(TestConfig.DbContexts, TestConfig.Mapper);

            var qtdExptectd = 42;
            var result = await request.Handle(query, CancellationToken.None);

            Assert.Equal(result.Count(), qtdExptectd);
        }
    }
}
