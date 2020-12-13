using System.ComponentModel.DataAnnotations;

namespace MoviesApi.ViewModels.Request
{
    public class AddGenreRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
