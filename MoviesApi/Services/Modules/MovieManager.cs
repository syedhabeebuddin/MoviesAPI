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
using System.Threading.Tasks;

namespace MoviesApi.Services.Modules
{
    public class MovieManager : IMovieManager
    {
        private readonly IManageCollection<Movie> _movieCollection;
        private readonly IManageCollection<Genre> _genreCollection;
        private readonly IGenreManager _genreManager;
        public MovieManager(IManageCollection<Movie> movieCollection, 
            IManageCollection<Genre> genreCollection,IGenreManager genreManager)
        {
            _movieCollection = movieCollection;
            _genreCollection = genreCollection;
            _genreManager = genreManager;
        }

        public async Task<MovieResponse> GetByIdAsync(string id)
        {
            var result = await _movieCollection.GetbyIdAsync(id);
            return result != null ? new MovieResponse(result) : null;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllAsync()
        {
            var result = await _movieCollection.GetAllAsync();            
            return GetMovieResponse(result);
        }

        private IEnumerable<MovieResponse> GetMovieResponse(IEnumerable<Movie> result)
        {
            List<MovieResponse> movieResponses = null;
            if (!(result is null))
            {
                movieResponses = new List<MovieResponse>();
                foreach (Movie move in result)
                {
                    movieResponses.Add(new MovieResponse(move));
                }
            }

            return movieResponses;
        }

        public async Task<Result> InsertAsync(AddMovieRequest request)
        {
            Movie movie = new Movie();
            movie.Name = request.Name;
            movie.Description = request.Description;
            movie.Duration = request.Duration;
            movie.Rating = request.Rating;
            movie.ReleaseDate = request.ReleaseDate;
            movie.Genres = request.Genres;

            try
            {
                await VerifyGenres(request.Genres);
                await _movieCollection.InsertAsync(movie);
            }
            catch (MongoWriteException)
            {
                return new Result(false, "Movie Already Exists", StatusCodes.Status409Conflict);
            }

            return string.IsNullOrEmpty(movie.Id) ? new Result(false, "Failed To Add Movie", StatusCodes.Status500InternalServerError) : new Result(true, string.Empty);
        }

        public async Task<Result> UpdateAsync(string id, UpdateMovieRequest updateRequest)
        {
            var updatesFilter = Builders<Movie>.Filter.Eq(t => t.Id, id);

            var updates = Builders<Movie>
                          .Update
                          .Set("Description", updateRequest.Description)
                          .Set("ReleaseDate", updateRequest.ReleaseDate)
                          .Set("Rating", updateRequest.Rating)
                          .Set("Duration", updateRequest.Duration);

            var updateResult = await _movieCollection.UpdatePartialAsync(updatesFilter, updates);

            return ApiUtils.ProcessResult(updateResult);
        }

        public async Task<Result> DeleteAsync(string id)
        {
            try
            {
                await _movieCollection.DeleteAsync(id);
            }
            catch (MongoException)
            {
                return new Result(false, "Movie Doesn't Exists", StatusCodes.Status416RangeNotSatisfiable);
            }
            return new Result(true, string.Empty);
        }

        private async Task<List<Genre>> GetGenres(List<AddGenreRequest> genres)
        {
            List<Genre> lstGenres = new List<Genre>();
            string id = string.Empty;
            foreach (AddGenreRequest req in genres)
            {
                var genre = new Genre();
                genre.Name = req.Name;
                genre.Description = req.Description;
                var res = await _genreManager.SearchAsync(new SearchRequest { fieldName = "Name", fieldValue = req.Name });
                if(res == null)
                {
                   await _genreManager.InsertAsync(req);
                   res = await _genreManager.SearchAsync(new SearchRequest { fieldName = "Name", fieldValue = req.Name });
                    //id= req.id
                }
                genre.Id = res.Id;
                lstGenres.Add(genre);
            }

            return lstGenres;
        }

        private async Task VerifyGenres(List<string> genres)
        {            
            string id = string.Empty;

            foreach (string genre in genres)
            {                
                var res = await _genreManager.SearchAsync(new SearchRequest { fieldName = "Name", fieldValue = genre });
                if (res == null)
                {
                    var req = new AddGenreRequest { Name = genre, Description = string.Empty };
                    await _genreManager.InsertAsync(req);
                }
            }
        }
    }
}
