using MoviesApi.Core.Common;
using MoviesApi.ViewModels.Request;
using MoviesApi.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Services.Contracts
{
    public interface IMovieManager
    {
        Task<MovieResponse> GetByIdAsync(string id);
        Task<IEnumerable<GenreResponse>> GetAllAsync();
        Task<Result> InsertAsync(AddMovieRequest request);
        Task<Result> UpdateAsync(string id, UpdateMovieRequest updateRequest);
        Task<Result> DeleteAsync(string id);
    }
}
