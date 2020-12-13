using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoviesApi.Core.Models
{
    public class MongoDoc
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]        
        public string Id { get; set; }
    }
}
