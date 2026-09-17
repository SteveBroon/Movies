using Movies.Domain.Entities;

namespace Movies.Application.Responses.Movies;

public record MovieSearchResponse
(
    Guid Id,
    string Title,
    DateTime? ReleaseDate,
    string? Overview,
    decimal? Popularity,
    int? VoteCount,
    decimal? VoteAverage,
    string? OriginalLanguage,
    string? PosterUrl,

    IEnumerable<string> Genres 
)
{
    public static MovieSearchResponse FromEntity(Movie movie)
    {
        return new MovieSearchResponse
        (
            movie.Id,
            movie.Title,
            movie.ReleaseDate,
            movie.Overview,
            movie.Popularity,
            movie.VoteCount,
            movie.VoteAverage,
            movie.OriginalLanguage,
            movie.PosterUrl,
            movie.MovieGenres.Select(x => x.Genre.Name).ToArray()
        );
    }
}