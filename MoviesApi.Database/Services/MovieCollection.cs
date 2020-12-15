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
    public class MovieCollection : IManageCollection<Movie>
    {
        private readonly IMongoCollection<Movie> _movieCollection;

        private readonly IManageDatabase _database;
        public IMongoQueryable<Movie> Queryable { get; set; }

        public MovieCollection(IManageDatabase database, IOptions<MongoDBSettings> mongoDBSettings)
        {
            _database = database;
            _movieCollection = _database.ApplicationDB.GetCollection<Movie>("movies");

            var options = new CreateIndexOptions() { Unique = true, Collation = new Collation("en", strength: CollationStrength.Primary) };
            Queryable = _movieCollection.AsQueryable();
        }

        public Task DeleteAsync(string id)
        {
            return _movieCollection.DeleteOneAsync(Builders<Movie>.Filter.Eq(x => x.Id, id));
        }

        public async Task<IEnumerable<Movie>> GetAsync(FilterDefinition<Movie> filters, ProjectionDefinition<Movie> fields = null)
        {
            return fields == null ? await _movieCollection.Find(filters).ToListAsync() : await _movieCollection.Find(filters).Project<Movie>(fields).ToListAsync();
        }

        public async Task<Movie> GetbyIdAsync(string id)
        {
            return await _movieCollection.Find(Builders<Movie>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var result = await _movieCollection.Find(_ => true).ToListAsync();
            return result;

            //return (IEnumerable<Movie>)await _movieCollection.FindAsync(new BsonDocument());
            //return fields == null ? await _genreCollection.Find(filters).ToListAsync() : await _genreCollection.Find(filters).Project<Genre>(fields).ToListAsync();
        }
        public async Task<Movie> Search(FilterDefinition<Movie> filters)
        {
            return await _movieCollection.Find(filters).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Movie movie)
        {
            await _movieCollection.InsertOneAsync(movie);
            if (string.IsNullOrEmpty(movie.Id))
            {
                throw new Exception($"Failed to save the movie: {JsonSerializer.Serialize(movie)}");
            }
        }

        public async Task<UpdateResult> UpdatePartialAsync(FilterDefinition<Movie> filters, UpdateDefinition<Movie> updates)
        {
            return await _movieCollection.UpdateOneAsync(filters, updates);
        }

        public async Task<Movie> GetbyIdAsync(FilterDefinition<Movie> filters)
        {
            return await _movieCollection.Find(filters).FirstOrDefaultAsync();
        }
    }
}
