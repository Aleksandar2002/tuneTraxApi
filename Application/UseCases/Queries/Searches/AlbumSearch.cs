namespace Application.UseCases.Queries.Searches;

public class AlbumSearch : PagedSearch
{
    public string? Name { get; set; }
    public string? Artist { get; set; }
}
