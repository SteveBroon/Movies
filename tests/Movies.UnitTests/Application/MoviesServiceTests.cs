using Moq;
using Movies.Application.Interfaces;
using Movies.Application.Services;
using Movies.Domain.Entities;

namespace Movies.UnitTests.Application;

public class MoviesServiceTests
{
    protected readonly Mock<IMovieRepository> _movieRepository;
    protected readonly MovieService _movieService;

    public MoviesServiceTests()
    {
        _movieRepository = new Mock<IMovieRepository>();
        _movieService = new MovieService(_movieRepository.Object);
    }

    public class GetMovieByIdAsyncTest : MoviesServiceTests
    {
        [Fact]
        public async Task ReturnMovie_WhenIdExists()
        {
            // Given
        
            // When
        
            // Then
        }

        [Fact]
        public async Task ReturnNull_WhenIdDoesNotExist()
        {
            var movieId = Guid.NewGuid();

            _movieRepository
                .Setup(x => x.GetByIdAsync(
                    movieId,
                    It.IsAny<CancellationToken>())
                )
                .ReturnsAsync((Movie?)null);

            // Act
            var result = await _movieService.GetMovieByIdAsync(
                movieId,
                CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}