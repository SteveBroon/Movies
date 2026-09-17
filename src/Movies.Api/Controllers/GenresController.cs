using Microsoft.AspNetCore.Mvc;
using Movies.Application.Interfaces;
using Movies.Application.Responses.Genres;

namespace Movies.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreSevice _genreService;

    public GenresController(IGenreSevice genreSevice)
    {
        _genreService = genreSevice;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<GenreResonse>>> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await _genreService.GetGenresAsync(cancellationToken));
    }
}