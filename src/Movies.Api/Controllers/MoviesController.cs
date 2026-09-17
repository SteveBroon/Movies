using Microsoft.AspNetCore.Mvc;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieSevice _movieService;

    public MoviesController(IMovieSevice movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult> GetMovies(
        [FromQuery] MovieSearchRequest request,
        CancellationToken cancellationToken)
    {
        var movies = await _movieService.GetMoviesAsync(
            request,
            cancellationToken);

        return Ok(movies);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetMovie(
        Guid id,
        CancellationToken cancellationToken)
    {
        var item = await _movieService.GetMovieByIdAsync(
            id,
            cancellationToken);

        return item is null
            ? NotFound()
            : Ok(item);
    }
}