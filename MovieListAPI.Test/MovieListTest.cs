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
            Main main = new Main
            {
                min = new List<Min>
                {
                   new Min
                   {
                       id = 87,
                       producer = "Wyck Godfrey, Stephenie Meyer and Karen Rosenfelt",
                       interval = 1,
                       previousWin = 2011,
                       followingWin = 2012
                   }
                },
                max = new List<Max>
                {
                    new Max
                    {
                        id = 180,
                        producer = "Jerry Weintraub",
                        interval = 18,
                        previousWin = 1980,
                        followingWin = 1998
                    }
                },
            };

            var query = new MovieListCommand();

            var request = new MovieListHandler(TestConfig.DbContext);

            var result = await request.Handle(query, CancellationToken.None);

            Assert.Equal(main, result);
        }

        [Fact]
        public async Task GetListMovieTest()
        {
            var query = new ListMovieCommand();

            var request = new ListMovieHandler(TestConfig.DbContext, TestConfig.Mapper);

            var result = await request.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetWinnersProducersTest()
        {
            var query = new ListWinnerMovieListCommand();

            var request = new ListWinnerMovieListHandler(TestConfig.DbContext, TestConfig.Mapper);

            var qtdExptectd = 42;
            var result = await request.Handle(query, CancellationToken.None);

            Assert.Equal(result.Count(), qtdExptectd);
        }
    }
}
