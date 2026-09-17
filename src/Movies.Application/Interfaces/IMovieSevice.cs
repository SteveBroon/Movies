using Movies.Application.Responses.Movies;

namespace Movies.Application.Interfaces;

public interface IMovieSevice
{
    Task<IReadOnlyCollection<MovieSearchResponse>> GetMoviesAsync(MovieSearchRequest request, CancellationToken cancellationToken);
    Task<MovieSearchResponse?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken);
}
