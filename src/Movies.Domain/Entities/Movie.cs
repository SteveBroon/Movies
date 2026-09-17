namespace Movies.Domain.Entities;

public class Movie
{
    public Guid Id { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Overview { get; set; }
    public decimal? Popularity { get; set; }
    public int? VoteCount { get; set; }
    public decimal? VoteAverage { get; set; }
    public string? OriginalLanguage { get; set; }
    public string? PosterUrl { get; set; }
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
}