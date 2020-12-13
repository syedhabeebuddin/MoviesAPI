namespace MoviesApi.Core.Models
{
    public class Genre : MongoDoc
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
