using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Response
{
    public class MovieResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Genres { get; set; }
        public int Duration { get; set; }
        public int Rating { get; set; }

        public MovieResponse(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.ReleaseDate = movie.ReleaseDate;            
            this.Duration = movie.Duration;
            this.Rating = movie.Rating;
            this.Genres = movie.Genres;

            //this.Genres = new List<GenreResponse>();
            //foreach (var item in movie.Genres)
            //{
            //    var genre = new GenreResponse(item);
            //    Genres.Add(genre);
            //}
        }
    }
}
