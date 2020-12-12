using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Core.Models
{
    public class Movie : MongoDoc
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        //public List<Genre> Genres { get; set; }
        public List<string> Genres { get; set; }
        public int Duration { get; set; }
        public int Rating { get; set; }
    }
}
