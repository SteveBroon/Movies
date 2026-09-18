using Movies.Application.Responses.Genres;

namespace Movies.Application.Interfaces
{
    public interface IGenreSevice
    {
        Task<IReadOnlyCollection<GenreResponse>> GetGenresAsync(CancellationToken cancellationToken);
    }
}
