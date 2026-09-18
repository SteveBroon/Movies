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

    public class GetMoviesAsyncTests : MoviesServiceTests
    {
        [Fact]
        public async Task ReturnPagedResult_WhenMoviesExist()
        {
            // Setup
            var request = new MovieSearchRequest
            {
                Search = "Test",
                Genre = 1,
                SortBy = "Title",
                Descending = false,
                Page = 1,
                PageSize = 10
            };

            var movies = new List<Movie>
            {
                new Movie { Id = Guid.NewGuid(), Title = "Test Movie 1" },
                new Movie { Id = Guid.NewGuid(), Title = "Test Movie 2" }
            };

            _movieRepository
                .Setup(x => x.SearchAsync(
                    request.Search,
                    request.Genre,
                    request.SortBy,
                    request.Descending,
                    request.Page,
                    request.PageSize,
                    It.IsAny<CancellationToken>())
                )
                .ReturnsAsync((movies, movies.Count));

            // Act
            var result = await _movieService.GetMoviesAsync(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Page, result.Page);
            Assert.Equal(request.PageSize, result.PageSize);
            Assert.Equal(movies.Count, result.TotalCount);
            Assert.Equal(movies.Count, result.Items.Count());
        }

        [Fact]
    public async Task WhenNoMoviesFound_ReturnsEmptyResult()
    {
        // Arrange
        var request = new MovieSearchRequest
        {
            Page = 1,
            PageSize = 20
        };

        _movieRepository
            .Setup(x => x.SearchAsync(
                It.IsAny<string?>(),
                It.IsAny<int?>(),
                It.IsAny<string?>(),
                It.IsAny<bool>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((
                new List<Movie>(),
                0));

        // Act
        var result = await _movieService.GetMoviesAsync(
            request,
            CancellationToken.None);

        // Assert
        Assert.Empty(result.Items);
        Assert.Equal(0, result.TotalCount);
        Assert.Equal(0, result.TotalPages);
    }
    }

    public class GetMovieByIdAsyncTests : MoviesServiceTests
    {
        [Fact]
        public async Task ReturnMovie_WhenIdExists()
        {
            // Setup
            var movieId = Guid.NewGuid();

            var movie = new Movie
                {
                    Id = movieId,
                    Title = "Test Movie",
                    
                    Overview = "Test Description",
                    ReleaseDate = DateTime.UtcNow,
                    MovieGenres = new List<MovieGenre>
                    {
                        new MovieGenre
                        {
                            Genre = new Genre
                            {
                                Id = 1,
                                Name = "Action"
                            }
                        },
                        new MovieGenre
                        {
                            Genre = new Genre
                            {
                                Id = 2,
                                Name = "Action"
                            }
                        }
                    }
                };

            _movieRepository
                .Setup(x => x.GetByIdAsync(
                    movieId,
                    It.IsAny<CancellationToken>())
                )
                .ReturnsAsync(movie);

            // Act
            var result = await _movieService.GetMovieByIdAsync(
                movieId,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movieId, result.Id);
            Assert.Equal(movie.Title, result.Title);
            Assert.Equal(movie.Overview, result.Overview);
            Assert.Equal(movie.ReleaseDate, result.ReleaseDate);
            Assert.Equal(movie.MovieGenres.Count, result.Genres.Count());
        }

        [Fact]
        public async Task ReturnNull_WhenIdDoesNotExist()
        {
            // Setup
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