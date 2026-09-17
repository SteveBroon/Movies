using Microsoft.AspNetCore.Mvc;
using Movies.Application.Interfaces;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController(IMovieSevice movieService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetMovies(
            [FromQuery] MovieSearchRequest request,
            CancellationToken cancellationToken)
        {
            var movies = await movieService.GetMoviesAsync(
                request,
                cancellationToken);

            return Ok(movies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetMovie(
            Guid id,
            CancellationToken cancellationToken)
        {
            var item = await movieService.GetMovieByIdAsync(
                id,
                cancellationToken);

            return item is null
                ? NotFound()
                : Ok(item);
        }
    }
}