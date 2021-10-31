using MediatR;
using System.Collections.Generic;

namespace MovieList.Core.MovieList.ListMovieList.Handlers.Queries
{
    public class ListMovieCommand : IRequest<IEnumerable<ViewModel.MovieList.MovieList>>
    {
    }
}
