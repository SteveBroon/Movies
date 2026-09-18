using Moq;
using Movies.Application.Interfaces;
using Movies.Application.Services;
using Movies.Domain.Entities;

namespace Movies.UnitTests.Application
{
    public class GenreServiceTests
    {
        protected readonly Mock<IGenreRepository> _genreRepository;
        protected readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _genreRepository = new Mock<IGenreRepository>();
            _genreService = new GenreService(_genreRepository.Object);
        }

        public class GetGenresAsyncTests : GenreServiceTests
        {
            [Fact]
            public async Task ReturnGenres_WhenGenresExist()
            {
                // Setup
                var genres = new List<Genre>
                {
                    new Genre { Id = 1, Name = "Action" },
                    new Genre { Id = 2, Name = "Comedy" }
                };

                _genreRepository
                    .Setup(x => x.GetGenresAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(genres);

                // Act
                var result = await _genreService.GetGenresAsync(CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(genres.Count, result.Count);
            }
        }

    }
}
