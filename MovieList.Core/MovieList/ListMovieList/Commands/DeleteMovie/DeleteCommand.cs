using MediatR;

namespace MovieList.Core.MovieList.ListMovieList.Commands.DeleteMovie
{
    public class DeleteCommand : IRequest<bool>
    {
        public int id { get; set; }
    }
}
