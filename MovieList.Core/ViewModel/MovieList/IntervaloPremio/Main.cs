using System;
using System.Collections.Generic;

namespace MovieList.Core.ViewModel.MovieList.IntervaloPremio
{
    public class Main
    {
        public virtual ICollection<Min> min
        {
            get; set;
        }
        public virtual ICollection<Max> max
        {
            get; set;
        }

        public static implicit operator Main(Domain.Entities.IntervaloPremio.Main v)
        {
            throw new NotImplementedException();
        }
    }
}
