using Movies.Application.Interfaces;
using Movies.Application.Responses.Genres;

namespace Movies.Application.Services;

public class GenreService : IGenreSevice
{
    public IGenreRepository GenreRepository { get; }

    public GenreService(IGenreRepository genreRepository)
    {
        GenreRepository = genreRepository;
    }

    public async Task<IReadOnlyCollection<GenreResonse>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var genres = await GenreRepository.GetGenresAsync(cancellationToken).ConfigureAwait(false);

        return genres.Select(genre => GenreResonse.FromEntity(genre)).ToList();
    }
}
