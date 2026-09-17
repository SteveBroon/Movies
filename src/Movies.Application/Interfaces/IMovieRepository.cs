using Movies.Domain.Entities;

namespace Movies.Application.Interfaces;

public interface IMovieRepository
{
    Task<IReadOnlyList<Movie>> SearchAsync(
        string? search,
        int? genre,
        string? sortBy,
        bool descending,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    );

    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}