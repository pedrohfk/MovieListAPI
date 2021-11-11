using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MovieList.Core.ViewModel.MovieList.IntervaloPremio
{
    public class Max
    {
        [JsonIgnore]
        public int id { get; set; }
        public string producer { get; set; }
        public int interval { get; set; }
        public int previousWin { get; set; }
        public int followingWin { get; set; }
    }
}
