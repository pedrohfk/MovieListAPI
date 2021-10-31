using MediatR;
using System.Collections.Generic;

namespace MovieList.Core.MovieList.ListWinnerMovieList.Queries
{
    public class ListWinnerMovieListCommand : IRequest<IEnumerable<ViewModel.MovieList.MovieList>>    
    {
    }
}
