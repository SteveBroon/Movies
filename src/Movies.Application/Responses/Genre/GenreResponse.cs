using Movies.Domain.Entities;

namespace Movies.Application.Responses.Genres;

public record GenreResonse
(
    int Id,
    string Name
)
{
    public static GenreResonse FromEntity(Genre genre)
    {
        return new GenreResonse(
            genre.Id,
            genre.Name
        );
    }
}