using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MoviesApi.Core.Configurations;
using MoviesApi.Core.Models;
using MoviesApi.Database.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesApi.Database.Services
{
    public class GenreCollection : IManageCollection<Genre>
    {
        private readonly IMongoCollection<Genre> _genreCollection;

        private readonly IManageDatabase _database;
        public IMongoQueryable<Genre> Queryable { get; set; }

        public GenreCollection(IManageDatabase database, IOptions<MongoDBSettings> mongoDBSettings)
        {
            _database = database;
            _genreCollection = _database.ApplicationDB.GetCollection<Genre>("genres");

            var options = new CreateIndexOptions() { Unique = true, Collation = new Collation("en", strength: CollationStrength.Primary) };
            Queryable = _genreCollection.AsQueryable();
        }

        public Task DeleteAsync(string id)
        {
            return _genreCollection.DeleteOneAsync(Builders<Genre>.Filter.Eq(x => x.Id, id));
        }

        public async Task<IEnumerable<Genre>> GetAsync(FilterDefinition<Genre> filters, ProjectionDefinition<Genre> fields = null)
        {
            return fields == null ? await _genreCollection.Find(filters).ToListAsync() : await _genreCollection.Find(filters).Project<Genre>(fields).ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var result = await _genreCollection.FindAsync(new BsonDocument());
            return null;
            //return (IEnumerable<Genre>)
            //return fields == null ? await _genreCollection.Find(filters).ToListAsync() : await _genreCollection.Find(filters).Project<Genre>(fields).ToListAsync();
        }

        public async Task<Genre> GetbyIdAsync(string id)
        {
            return await _genreCollection.Find(Builders<Genre>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<Genre> Search(FilterDefinition<Genre> filters)
        {
            return await _genreCollection.Find(filters).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Genre genre)
        {
            await _genreCollection.InsertOneAsync(genre);
            if (string.IsNullOrEmpty(genre.Id))
            {
                throw new Exception($"Failed to save the movie: {JsonSerializer.Serialize(genre)}");
            }
        }

        public async Task<UpdateResult> UpdatePartialAsync(FilterDefinition<Genre> filters, UpdateDefinition<Genre> updates)
        {
            return await _genreCollection.UpdateOneAsync(filters, updates);
        }

        public async Task<Genre> GetbyIdAsync(FilterDefinition<Genre> filters)
        {
            return await _genreCollection.Find(filters).FirstOrDefaultAsync();
        }
    }
}
