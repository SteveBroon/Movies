using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Interfaces;
using Movies.Application.Services;

namespace Movies.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IMovieSevice, MovieService>();
        services.AddScoped<IGenreSevice, GenreService>();

        return services;
    }
}