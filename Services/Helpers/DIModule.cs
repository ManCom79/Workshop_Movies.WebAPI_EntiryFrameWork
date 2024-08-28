using DataAccess;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Helpers
{
    public static class DIModule
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesDbContext>(
                    opts => opts.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            return services;
        }
    }
}
