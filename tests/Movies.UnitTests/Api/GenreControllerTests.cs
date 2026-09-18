using Microsoft.AspNetCore.Mvc;
using Moq;
using Movies.Api.Controllers;
using Movies.Application.Interfaces;
using Movies.Application.Responses.Genres;

namespace Movies.UnitTests.Api;

public class GenreControllerTests
{
    private readonly Mock<IGenreSevice> _genreService;
    private readonly GenresController _controller;

    public GenreControllerTests()
    {
        _genreService = new Mock<IGenreSevice>();

        _controller = new GenresController(
            _genreService.Object);
    }


    public class GetAllTests : GenreControllerTests
    {
        [Fact]
        public async Task ReturnGenres_WhenGenresExist()
        {
            // Setup
            var genres = new List<GenreResponse>
            {
                new GenreResponse(1, "Action"),
                new GenreResponse(2, "Comedy")  
            };

            _genreService
                .Setup(x => x.GetGenresAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(genres);

            // Act
            var result = await _controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedGenres = Assert.IsAssignableFrom<IReadOnlyCollection<GenreResponse>>(okResult.Value);
            Assert.Equal(genres.Count, returnedGenres.Count);
        }
    }
}