using Microsoft.AspNetCore.Mvc;
using Movies.Application.Interfaces;
using Movies.Application.Responses.Genres;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController(IGenreSevice genreSevice) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<GenreResonse>>> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await genreSevice.GetGenresAsync(cancellationToken));
        }
    }
}