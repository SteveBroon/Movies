using Movies.Application.Common;
using Movies.Application.Interfaces;
using Movies.Application.Responses.Movies;

namespace Movies.Application.Services;

public class MovieService : IMovieSevice
{
    public IMovieRepository MovieRepository { get; }

    public MovieService(IMovieRepository movieRepository)
    {
        MovieRepository = movieRepository;
    }

    public async Task<PagedResult<MovieSearchResponse>> GetMoviesAsync(MovieSearchRequest request, CancellationToken cancellationToken)
    {
        var (movies, totalCount) = await MovieRepository.SearchAsync(request.Search, request.Genre, request.SortBy, request.Descending, request.Page, request.PageSize, cancellationToken);

        return new PagedResult<MovieSearchResponse>()
        {
            Items = movies.Select(movie => MovieSearchResponse.FromEntity(movie)).ToList(),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<MovieSearchResponse?> GetMovieByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await MovieRepository.GetByIdAsync(id, cancellationToken);
        return item is null
            ? null
            : MovieSearchResponse.FromEntity(item);
    }
}
