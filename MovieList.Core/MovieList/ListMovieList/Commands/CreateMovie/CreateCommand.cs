using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieList.Core.MovieList.ListMovieList.Commands.CreateMovie
{
    public class CreateCommand : IRequest<ViewModel.MovieList.MovieList>
    {
        public int id { get; set; }

        public string year { get; set; }

        public string title { get; set; }

        public string studios { get; set; }

        public string producers { get; set; }

        public string winner { get; set; }
    }
}
