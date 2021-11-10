using AutoMapper;
using MovieList.Core.MovieList.ListMovieList.Commands.CreateMovie;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieList.Core.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Domain.Entities.MovieList, ViewModel.MovieList.MovieList>();
            CreateMap<CreateCommand, Domain.Entities.MovieList>();

            CreateMap<Domain.Entities.IntervaloPremio.Min, ViewModel.MovieList.IntervaloPremio.Min>();
            CreateMap<Domain.Entities.IntervaloPremio.Max, ViewModel.MovieList.IntervaloPremio.Max>();

            CreateMap<ViewModel.MovieList.IntervaloPremio.Min, ViewModel.MovieList.IntervaloPremio.Main>();
            CreateMap<ViewModel.MovieList.IntervaloPremio.Max, ViewModel.MovieList.IntervaloPremio.Main>();

        }
    }
}
