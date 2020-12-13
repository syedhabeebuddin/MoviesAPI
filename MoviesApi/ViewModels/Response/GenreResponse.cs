using MoviesApi.Core.Models;

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
