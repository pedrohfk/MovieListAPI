using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Persistencia;
using System.Threading;
using System.Threading.Tasks;

namespace MovieList.Core.MovieList.ListMovieList.Commands.DeleteMovie
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        private DbContextPersistencia ContextDb { get; }

        private DeleteCommandValidator Validator { get; }

        public DeleteCommandHandler(DbContextPersistencia contextDb)
        {
            this.ContextDb = contextDb;
            this.Validator = new DeleteCommandValidator(contextDb);
        }

        public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            Validator.ValidateAndThrow(request);

            var result = await ContextDb.MovieList.FirstOrDefaultAsync(i => i.id == request.id);

            ContextDb.Remove(result);

            await ContextDb.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
