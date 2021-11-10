using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entities.IntervaloPremio;
using MovieList.Persistencia;
using System;
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
               "CREATE TEMP TABLE MAX AS " +
                "SELECT MAX(id) AS id, " +
                "       MAX(ml.producers) AS producer, " +
                "       MAX(ml.year) - MIN(ml.year) AS interval, " +
                "       MIN(ml.year) AS previousWin," +
                "       MAX(ml.year) AS followingWin " +
                "FROM 'MovieList' ml " +
                "GROUP BY ml.producers " +
                "HAVING MAX(ml.year) - MIN(ml.year) > 1; " +
                "SELECT * FROM TEMP.MAX WHERE interval = (SELECT MAX(interval) FROM TEMP.MAX)" +
                "").ToListAsync();


            List<ViewModel.MovieList.IntervaloPremio.Max> max = produtorMaiorIntervaloEntreDoisPremios.ConvertAll(i => new ViewModel.MovieList.IntervaloPremio.Max
            {
                id = i.id,
                producer = i.producer,
                interval = i.interval,
                previousWin = i.previousWin,
                followingWin = i.followingWin
            });
        

            var result = new ViewModel.MovieList.IntervaloPremio.Main
            {
                min = min,
                max = max
            };

            return result;
        }
    }
}
