using CsvHelper.Configuration.Attributes;

namespace MovieList.Domain.Entities
{
    public class MovieList
    {
        [Ignore]
        public int id { get; set; }               
      
        public string year { get; set; }              
     
        public string title { get; set; }               
   
        public string studios { get; set; }
       
        public string producers { get; set; }
        
        public string winner { get; set; }

    }
}
