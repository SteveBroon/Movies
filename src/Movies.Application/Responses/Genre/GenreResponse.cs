using Movies.Domain.Entities;

namespace Movies.Application.Responses.Genres
{
    public record GenreResponse
    (
        int Id,
        string Name
    )
    {
        public static GenreResponse FromEntity(Genre genre)
        {
            return new GenreResponse(
                genre.Id,
                genre.Name
            );
        }
    }
}