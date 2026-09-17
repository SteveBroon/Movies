using Microsoft.EntityFrameworkCore;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Persistence
{
    public class GenreRepository(ApplicationDbContext context) : IGenreRepository
    {
        public async Task<IReadOnlyCollection<Genre>> GetGenresAsync(CancellationToken cancellationToken)
        {
            return await context.Genres
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}