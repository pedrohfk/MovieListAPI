using AutoMapper;
using MediatR;
using MovieList.Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieList.Core.MovieList.ListMovieList.Commands.CreateMovie
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, ViewModel.MovieList.MovieList>
    {

        private DbContextPersistencia ContextDb { get; }
        private IMapper Mapper { get; }

        public CreateCommandHandler(DbContextPersistencia contextDb, IMapper mapper)
        {
            this.ContextDb = contextDb;
            this.Mapper = mapper;
        }

        public async Task<ViewModel.MovieList.MovieList> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var result = Mapper.Map<Domain.Entities.MovieList>(request);

            await ContextDb.AddAsync(result, cancellationToken);
            await ContextDb.SaveChangesAsync(cancellationToken);

            return Mapper.Map<ViewModel.MovieList.MovieList>(result);
        }
    }
}
