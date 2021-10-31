using System;
using System.Collections.Generic;
using System.Text;

namespace MovieList.Domain.Entities.IntervaloPremio
{
    public class Min
    {
        public int id { get; set; }
        public string producer { get; set; }
        public int interval { get; set; }
        public int previousWin { get; set; }
        public int followingWin { get; set; }
    }
}
