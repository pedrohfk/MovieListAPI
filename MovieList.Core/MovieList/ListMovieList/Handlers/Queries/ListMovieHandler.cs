using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Persistencia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieList.Core.MovieList.ListMovieList.Handlers.Queries
{
    public class ListMovieHandler : IRequestHandler<ListMovieCommand, IEnumerable<ViewModel.MovieList.MovieList>>
    {
        private readonly DbContextPersistencia contextDb;
        private readonly IMapper mapper;

        public ListMovieHandler(DbContextPersistencia context, IMapper mapper)
        {
            this.contextDb = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ViewModel.MovieList.MovieList>> Handle(ListMovieCommand request, CancellationToken cancellationToken)
        {
            var result = await contextDb.MovieList.ToListAsync();

            return mapper.Map<List<ViewModel.MovieList.MovieList>>(result);
        }    
    }
}
