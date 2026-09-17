using Movies.Application.Responses.Genres;

namespace Movies.Application.Interfaces
{
    public interface IGenreSevice
    {
        Task<IReadOnlyCollection<GenreResonse>> GetGenresAsync(CancellationToken cancellationToken);
    }
}
