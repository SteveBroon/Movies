using System.ComponentModel.DataAnnotations;

public class MovieSearchRequest
{
    public string? Search { get; set; }

    public int? Genre { get; set; }

    public string? SortBy { get; set; }

    public bool Descending { get; set; } = false;
    
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 20;
}