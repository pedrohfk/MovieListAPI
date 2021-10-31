using System.Collections.Generic;

namespace MovieList.Domain.Entities.IntervaloPremio
{
    public class Main
    {
        public virtual ICollection<Min> min { get; set; }
        public virtual ICollection<Max> max { get; set; }
    }
}
