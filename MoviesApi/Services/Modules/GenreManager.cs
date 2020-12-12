using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MoviesApi.Core.Common;
using MoviesApi.Core.Models;
using MoviesApi.Database.Contracts;
using MoviesApi.Services.Contracts;
using MoviesApi.Utils;
using MoviesApi.ViewModels.Request;
using MoviesApi.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Services.Modules
{
    public class GenreManager : IGenreManager
    {        
        private readonly IManageCollection<Genre> _genreCollection;
        public GenreManager(IManageCollection<Genre> genreCollection)
        {
            _genreCollection = genreCollection;

        }

        public async Task<GenreResponse> GetByIdAsync(string id)
        {
            var result = await _genreCollection.GetbyIdAsync(id);
            return result != null ? new GenreResponse(result) : null;
        }

        public async Task<IEnumerable<GenreResponse>> GetAllAsync()
        {
            //var scanFilter = Builders<ScanEngineDoc>.Filter;
            //var scanIdFilter = scanFilter.And(scanFilter.Eq(x => x.ScanId, scanId));

            var result = await _genreCollection.GetAllAsync();
            //return result != null ? new GenreResponse(result) : null;
            return null;
        }

        public async Task<GenreResponse> SearchAsync(SearchRequest searchRequest)
        {
            var searchFilter = GetSearchFilter(searchRequest); //Builders<Genre>.Filter.Eq(t => t., id)            
            var result = await _genreCollection.Search(searchFilter);

            return result != null ? new GenreResponse(result) : null;
        }

        private FilterDefinition<Genre> GetSearchFilter(SearchRequest searchRequest)
        {
            switch (searchRequest.fieldName.ToLower())
            {
                case "name":
                    return Builders<Genre>.Filter.Eq(t => t.Name, searchRequest.fieldValue);
                case "description":
                    return Builders<Genre>.Filter.Eq(t => t.Description, searchRequest.fieldValue);
                default:
                    return Builders<Genre>.Filter.Where(t => t.Id != null);
            }
        }

        public async Task<Result> InsertAsync(AddGenreRequest request)
        {
            Genre genre = new Genre();
            genre.Name = request.Name;
            genre.Description = request.Description;

            try
            {
                await _genreCollection.InsertAsync(genre);
            }
            catch (MongoWriteException)
            {                
                return new Result(false, "Genre Already Exists", StatusCodes.Status409Conflict);
            }

            return string.IsNullOrEmpty(genre.Id) ? new Result(false, "Failed To Add Genre", StatusCodes.Status500InternalServerError) : new Result(true, string.Empty);
        }

        public async Task<Result> UpdateAsync(string id,UpdateGenreRequest updateRequest)
        {
            var updatesFilter = Builders<Genre>.Filter.Eq(t => t.Id, id);

            var updates = Builders<Genre>
                          .Update
                          .Set("Name", updateRequest.Name)
                          .Set("Description", updateRequest.Description);
            var updateResult = await _genreCollection.UpdatePartialAsync(updatesFilter, updates);

            return ApiUtils.ProcessResult(updateResult);
        }

        public async Task<Result> DeleteAsync(string id)
        {
            try
            {
                await _genreCollection.DeleteAsync(id);
            }
            catch (MongoException)
            {
                return new Result(false, "Genre Doesn't Exists", StatusCodes.Status416RangeNotSatisfiable);
            }
            return new Result(true, string.Empty);
        }
    }
}
