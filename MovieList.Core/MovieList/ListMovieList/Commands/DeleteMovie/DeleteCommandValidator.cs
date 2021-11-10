using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieList.Core.MovieList.ListMovieList.Commands.DeleteMovie
{
    class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator(Persistencia.DbContextPersistencia dbContext)
        {
            RuleFor(i => i.id)
                .Must(i => dbContext.MovieList.Any(o => o.id == i))
                .WithMessage("Identificador não encontrado !!");
        }
    }
}
