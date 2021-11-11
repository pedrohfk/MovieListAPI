using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entities.IntervaloPremio;
using MovieList.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MovieList.Core.MovieList
{
    public class MovieListHandler : IRequestHandler<MovieListCommand, ViewModel.MovieList.IntervaloPremio.Main>
    {
        private readonly DbContextPersistencia contextDb;
    
        public MovieListHandler(DbContextPersistencia context)
        {
            this.contextDb = context;     
        }

        public async Task<ViewModel.MovieList.IntervaloPremio.Main> Handle(MovieListCommand request, CancellationToken cancellationToken)
        {
            var produtorObteveDoisPremiosMaisRapido = await contextDb.Min.FromSqlRaw<Min>("" +
                    "SELECT id AS id, " +
                    "       MAX(ml.producers) AS producer, " +
                    "       MAX(ml.year) - MIN(ml.year) AS interval, " +
                    "       MIN(ml.year) AS previousWin," +
                    "       MAX(ml.year) AS followingWin " +
                    "FROM 'MovieList' AS ml " +
                    "GROUP BY producers " +
                    "HAVING MAX(ml.year) - MIN(ml.year) = 1 " +
                    "ORDER BY MAX(ml.year) - MIN(ml.year) ASC").ToListAsync();

            List<ViewModel.MovieList.IntervaloPremio.Min> min = produtorObteveDoisPremiosMaisRapido.ConvertAll(i => new ViewModel.MovieList.IntervaloPremio.Min
            {
                id = i.id,
                producer = i.producer,
                interval = i.interval,
                previousWin = i.previousWin,
                followingWin = i.followingWin
            });

            var produtorMaiorIntervaloEntreDoisPremios = await contextDb.Max.FromSqlRaw(
                "SELECT MAX(id) AS id, " +
                "       MAX(ml.producers) AS producer, " +
                "       MAX(ml.year) - MIN(ml.year) AS interval, " +
                "       MIN(ml.year) AS previousWin," +
                "       MAX(ml.year) AS followingWin " +
                "FROM 'MovieList' ml " +
                "WHERE ml.winner = 'yes' " +
                "GROUP BY ml.producers ").ToListAsync();

            int previousWin, followingWin, BiggerDiff, previousWin_aux, followingWin_aux = 0;
            string producer, producer_biggerDiff = string.Empty;
            List<ViewModel.MovieList.IntervaloPremio.Max> max = new List<ViewModel.MovieList.IntervaloPremio.Max>();

            for (int x = 0; x < (produtorMaiorIntervaloEntreDoisPremios.Count() - 1); ++x)
            {
                BiggerDiff = 0;
                previousWin_aux = 0;
                followingWin = 0;
                previousWin = produtorMaiorIntervaloEntreDoisPremios[x].previousWin;
                producer = produtorMaiorIntervaloEntreDoisPremios[x].producer;

                for (int y = x + 1; y < produtorMaiorIntervaloEntreDoisPremios.Count(); ++y)
                {
                    if (produtorMaiorIntervaloEntreDoisPremios[y].producer.Contains(producer) && produtorMaiorIntervaloEntreDoisPremios[y].followingWin != previousWin)
                    {
                        followingWin = produtorMaiorIntervaloEntreDoisPremios[y].followingWin;
                    }

                    int diff = (followingWin - previousWin);

                    if (diff > BiggerDiff && diff != 0)
                    {
                        BiggerDiff = diff;
                        producer_biggerDiff = producer;
                        previousWin_aux = previousWin;
                        followingWin_aux = followingWin;

                        max.Add(new ViewModel.MovieList.IntervaloPremio.Max
                        {
                            producer = producer_biggerDiff,
                            interval = BiggerDiff,
                            followingWin = followingWin_aux,
                            previousWin = previousWin_aux
                        });      
                    }

                    max.RemoveAll(i => i.interval < BiggerDiff);
                }
            }

            var result = new ViewModel.MovieList.IntervaloPremio.Main
            {
                min = min,
                max = max
            };

            return result;
        }
    }
}
