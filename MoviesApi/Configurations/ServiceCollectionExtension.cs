using Microsoft.Extensions.DependencyInjection;
using MoviesApi.Services.Contracts;
using MoviesApi.Services.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi.Configurations
{
    public static class ServiceCollectionExtension
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IGenreManager, GenreManager>();
            services.AddScoped<IMovieManager, MovieManager>();

        }
    }
}
