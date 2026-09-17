using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface IGenreRepository
    {
         Task<IReadOnlyCollection<Genre>> GetGenresAsync(CancellationToken cancellationToken);
    }
}