using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Api.Controllers;
using Movies.Application.Common;
using Movies.Application.Interfaces;
using Movies.Application.Responses.Movies;

namespace Movies.UnitTests.Api;

public class MoviesControllerTests
{
    private readonly Mock<IMovieService> _movieService;
    private readonly MoviesController _controller;

    public MoviesControllerTests()
    {
        _movieService = new Mock<IMovieService>();

        _controller = new MoviesController(
            _movieService.Object);
    } 

    public class GetMoviesTests : MoviesControllerTests
    {
        [Fact]
        public async Task ReturnMovies_WhenMoviesExist()
        {
            // Setup
            var request = new MovieSearchRequest
            {
                Page = 1,
                PageSize = 10
            };

            var movies = new PagedResult<MovieSearchResponse>
            {
                Items = new List<MovieSearchResponse>
                {
                    new MovieSearchResponse(Guid.NewGuid(), "Movie 1", null, "Description 1", null, null, null, null, null, new List<string> { "Action", "Comedy" }),
                    new MovieSearchResponse(Guid.NewGuid(), "Movie 2", null, "Description 2", null, null, null, null, null, new List<string> { "Drama" })
                },
                TotalCount = 2
            };

            _movieService
                .Setup(x => x.GetMoviesAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movies);

            // Act
            var result = await _controller.GetMovies(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovies = Assert.IsAssignableFrom<PagedResult<MovieSearchResponse>>(okResult.Value);
            Assert.Equal(movies.TotalCount, returnedMovies.TotalCount);
        }

        [Fact]
        public async Task GetMovies_ShouldReturnEmpty()
        {
            // Setup
            var request = new MovieSearchRequest
            {
                Page = 1,
                PageSize = 10
            };

            var movies = new PagedResult<MovieSearchResponse>
            {
                Items = new List<MovieSearchResponse>(),
                TotalCount = 0
            };

            _movieService
                .Setup(x => x.GetMoviesAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movies);

            // Act
            var result = await _controller.GetMovies(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovies = Assert.IsAssignableFrom<PagedResult<MovieSearchResponse>>(okResult.Value);
            Assert.Empty(returnedMovies.Items);
        }
    }

    public class GetMovieTests : MoviesControllerTests
    {
        [Fact]
        public async Task ReturnMovie_WhenMovieExists()
        {
            // Setup
            var movieId = Guid.NewGuid();
            var movie = new MovieSearchResponse(movieId, "Movie 1", null, "Description 1", null, null, null, null, null, new List<string> { "Action", "Comedy" });

            _movieService
                .Setup(x => x.GetMovieByIdAsync(movieId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(movie);

            // Act
            var result = await _controller.GetMovie(movieId, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovie = Assert.IsAssignableFrom<MovieSearchResponse>(okResult.Value);
            Assert.Equal(movie.Id, returnedMovie.Id);
        }

        [Fact]
        public async Task ReturnNotFound_WhenMovieDoesNotExist()
        {
            // Setup
            var movieId = Guid.NewGuid();

            _movieService
                .Setup(x => x.GetMovieByIdAsync(movieId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((MovieSearchResponse?)null);

            // Act
            var result = await _controller.GetMovie(movieId, CancellationToken.None);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}