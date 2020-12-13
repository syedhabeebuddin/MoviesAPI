using MongoDB.Driver;

namespace MoviesApi.Database.Contracts
{
    public interface IManageConnection
    {
        public MongoClient MongoClient { get; set; }
    }
}
