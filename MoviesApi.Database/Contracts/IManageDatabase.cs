using MongoDB.Driver;

namespace MoviesApi.Database.Contracts
{
    public interface IManageDatabase
    {
        public IMongoDatabase ApplicationDB { get; set; }
    }
}
