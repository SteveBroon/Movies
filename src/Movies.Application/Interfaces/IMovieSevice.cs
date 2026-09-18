using Movies.Application.Common;
using Movies.Application.Responses.Movies;

namespace Movies.Application.Interfaces
{
    public interface IMovieService
    {
        Task<PagedResult<MovieSearchResponse>> GetMoviesAsync(MovieSearchRequest request, CancellationToken cancellationToken);
        Task<MovieSearchResponse?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
