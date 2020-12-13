using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Database.Contracts
{
    public interface IManageCollection<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(FilterDefinition<T> filters, ProjectionDefinition<T> fields = null);

        Task<T> GetbyIdAsync(string id);

        Task<T> GetbyIdAsync(FilterDefinition<T> filters);

        Task<T> Search(FilterDefinition<T> filters);

        Task InsertAsync(T val);

        Task DeleteAsync(string id);

        /// <summary>
        /// Helps to shoot wild queries out of the box just like linq
        /// </summary>
        public IMongoQueryable<T> Queryable { get; set; }

        Task<UpdateResult> UpdatePartialAsync(FilterDefinition<T> filters, UpdateDefinition<T> updates);
    }
}
