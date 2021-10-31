using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieList.Persistencia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MovieList.Core.Init
{

    public class InitHandler : IRequestHandler<Init, int>
    {
        private readonly DbContextPersistencia DBContext;

        public InitHandler(DbContextPersistencia dbContext)
        {
            this.DBContext = dbContext;
        }

       public async Task<int> Handle(Init request, CancellationToken cancellationToken)
       {
            await this.LoadMovieList();
            return 0;
       }
           
       public async Task LoadMovieList()
       {
           if (!await DBContext.MovieList.AnyAsync())
           {
                try
                {
                    var CurrentDirectory = Path.GetFullPath(".");

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                        Delimiter = ";"
                    };
                    using (var reader = new StreamReader(@$"{CurrentDirectory}\File\movielist.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        csv.Read();
                        csv.ReadHeader();
                        var records = csv.GetRecords<Domain.Entities.MovieList>();

                        List<Domain.Entities.MovieList> movies = csv.GetRecords<Domain.Entities.MovieList>().ToList();

                        await DBContext.MovieList.AddRangeAsync(movies);
                        await DBContext.SaveChangesAsync();
                    }
                }
                catch (System.Exception e)
                {

                    throw;
                }

            }
       }
    }
}
