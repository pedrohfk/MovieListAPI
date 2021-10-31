using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieList.Core.MovieList.ListWinnerMovieList.Queries
{
    public class ListWinnerMovieListHandler : IRequestHandler<ListWinnerMovieListCommand, IEnumerable<ViewModel.MovieList.MovieList>>
    {
        private readonly DbContextPersistencia contextDb;
        private readonly IMapper mapper;

        public ListWinnerMovieListHandler(DbContextPersistencia context, IMapper mapper)
        {
            this.contextDb = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ViewModel.MovieList.MovieList>> Handle(ListWinnerMovieListCommand request, CancellationToken cancellationToken)
        {
            var result = await contextDb.MovieList
                .Where(i => i.winner == "yes")
                .ToListAsync();

            return mapper.Map<List<ViewModel.MovieList.MovieList>>(result);
        }
    }
}
