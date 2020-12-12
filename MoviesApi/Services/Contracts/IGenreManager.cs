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
    public interface IGenreManager
    {        
        Task<GenreResponse> GetByIdAsync(string id);
        Task<IEnumerable<GenreResponse>> GetAllAsync();
        Task<GenreResponse> SearchAsync(SearchRequest searchRequest);
        Task<Result> InsertAsync(AddGenreRequest request);
        Task<Result> UpdateAsync(string id, UpdateGenreRequest updateRequest);
        Task<Result> DeleteAsync(string id);
    }
}
