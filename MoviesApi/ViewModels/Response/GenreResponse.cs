using MoviesApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.ViewModels.Response
{
    public class GenreResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GenreResponse(Genre genre)
        {
            this.Id = genre.Id;
            this.Name = genre.Name;
            this.Description = genre.Description;
        }
    }
}
