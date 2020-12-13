using System;

namespace MoviesApi.ViewModels.Request
{
    public class UpdateMovieRequest
    {   
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }        
        public int Duration { get; set; }
        public int Rating { get; set; }
    }
}
