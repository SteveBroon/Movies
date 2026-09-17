using Movies.Application.Interfaces;
using Movies.Application.Responses.Genres;

namespace Movies.Application.Services
{
    public class GenreService(IGenreRepository genreRepository) : IGenreSevice
    {
        public IGenreRepository GenreRepository { get; } = genreRepository;

        public async Task<IReadOnlyCollection<GenreResonse>> GetGenresAsync(CancellationToken cancellationToken)
        {
            var genres = await GenreRepository.GetGenresAsync(cancellationToken).ConfigureAwait(false);

            return genres.Select(genre => GenreResonse.FromEntity(genre)).ToList();
        }
    }
}
