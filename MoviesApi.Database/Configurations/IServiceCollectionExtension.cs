using Microsoft.Extensions.DependencyInjection;
using MoviesApi.Core.Models;
using MoviesApi.Database.Contracts;
using MoviesApi.Database.Services;

namespace MoviesApi.Database.Configurations
{
    public static class IServiceCollectionExtension
    {
        public static void AddDatabaseWrapper(this IServiceCollection services)
        {
            services.AddSingleton<IManageConnection, ManageConnection>();
            services.AddSingleton<IManageDatabase, ManageDatabase>();
            services.AddSingleton<IManageCollection<Movie>, MovieCollection>();
            services.AddSingleton<IManageCollection<Genre>, GenreCollection>();
        }
    }
}
