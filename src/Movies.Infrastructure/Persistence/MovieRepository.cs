using Microsoft.EntityFrameworkCore;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence
{
    public class MoviesRepository(ApplicationDbContext context) : IMovieRepository
    {
        public async Task<(IReadOnlyList<Movie>, int)> SearchAsync(
            string? search,
            int? genre,
            string? sortBy,
            bool descending,
            int page,
            int pageSize, CancellationToken cancellationToken)
        {
            var query = context.Movies
                .Include(movie => movie.MovieGenres)
                .ThenInclude(movieGenre => movieGenre.Genre)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(movie =>
                    EF.Functions.Like(movie.Title, $"%{search}%"));
            }

            if (genre is not null)
            {
                query = query.Where(movie =>
                    movie.MovieGenres.Any(movieGenre =>
                        movieGenre.Genre.Id == genre));
            }

            query = sortBy?.ToLowerInvariant() switch
            {
                "title" => descending
                    ? query.OrderByDescending(movie => movie.Title)
                    : query.OrderBy(movie => movie.Title),

                "releasedate" => descending
                    ? query.OrderByDescending(movie => movie.ReleaseDate)
                    : query.OrderBy(movie => movie.ReleaseDate),

                _ => query.OrderBy(movie => movie.Title)
            };

            // Get total count before pagination
            var totalCount = await query.CountAsync(
                cancellationToken);

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (result, totalCount);
        }

        public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Movies
                .AsNoTracking()
                .Include(movie => movie.MovieGenres)
                .ThenInclude(movieGenre => movieGenre.Genre)
                .FirstOrDefaultAsync(
                    movie => movie.Id == id,
                    cancellationToken
                );
        }
    }
}